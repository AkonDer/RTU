using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;

namespace RTU
{
    /// <summary>
    /// Класс в котором происходит основная работа программы
    /// </summary>
    class Model
    {
        Form1 form;
        Excel.Application excelapp;
        Excel.Workbooks excelappworkbooks;
        Excel.Workbook excelappworkbook;
        Excel.Sheets excelsheets;
        Excel.Worksheet excelworksheet;
        Excel.Range excelcells;

        /// <summary>
        /// количество строк в табличках с данными
        /// </summary>
        int numberstr; 

        /// <summary>
        /// Данные по РЛС
        /// </summary>
        struct RLS  
        {
            public string name;
            public double x;
            public double y;
            public double h;
        }
 
        /// <summary>
        /// Данные по ОП
        /// </summary>
        struct OP
        {
            public string name;
            public double x;
            public double y;
            public double h;
        }
        
        /// <summary>
        /// Данные по траектории
        /// </summary>
        struct DTr  
        {
            public int sec;
            public double x;
            public double y;
        }
        

        public Model(Form1 refer)  //конструктор
        {
            form = refer;
   
            DatGridV rlsDG = new DatGridV(form.dataGridViewRls, new string[] { "РЛС", "X", "Y", "H" }, 4, 9, true);
            DatGridV opDG = new DatGridV(form.dataGridViewOp, new string[] { "ОП", "X", "Y", "H" }, 4, 9, true);
            DatGridV datDG = new DatGridV(form.dataGridViewDTr, new string[] { "Секунда", "X", "Y" }, 3, 9, false);

            tabl();
        }

        RLS[] rls = new RLS[10];
        OP[] op = new OP[10];
        DTr[] dtr = new DTr[10];


        // Заполняем таблицу данными и присваиваем значения переменным
        void tabl()
        {
            DataSet dt = getTabl("SELECT name, x, y, h FROM rls");
            for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
            {
                form.dataGridViewRls[0, i].Value = rls[i].name = dt.Tables[0].Rows[i].ItemArray[0].ToString();
                form.dataGridViewRls[1, i].Value = rls[i].x = Convert.ToDouble(dt.Tables[0].Rows[i].ItemArray[1]);
                form.dataGridViewRls[2, i].Value = rls[i].y = Convert.ToDouble(dt.Tables[0].Rows[i].ItemArray[2]);
                form.dataGridViewRls[3, i].Value = rls[i].h = Convert.ToDouble(dt.Tables[0].Rows[i].ItemArray[3]);
            }

            dt = getTabl("SELECT name, x, y, h FROM op");
            for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
            {
                form.dataGridViewOp[0, i].Value = op[i].name = dt.Tables[0].Rows[i].ItemArray[0].ToString();
                form.dataGridViewOp[1, i].Value = op[i].x = Convert.ToDouble(dt.Tables[0].Rows[i].ItemArray[1]);
                form.dataGridViewOp[2, i].Value = op[i].y = Convert.ToDouble(dt.Tables[0].Rows[i].ItemArray[2]);
                form.dataGridViewOp[3, i].Value = op[i].h = Convert.ToDouble(dt.Tables[0].Rows[i].ItemArray[3]);
            }

            dt = getTabl("SELECT sec, x, y FROM dtr");
            for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
            {
                form.dataGridViewDTr[0, i].Value = Convert.ToInt32(dt.Tables[0].Rows[i].ItemArray[0].ToString());
                form.dataGridViewDTr[1, i].Value = Convert.ToDouble(dt.Tables[0].Rows[i].ItemArray[1]);
                form.dataGridViewDTr[2, i].Value = Convert.ToDouble(dt.Tables[0].Rows[i].ItemArray[2]);
            }
        }

        // получаем данные ид БД SQLite
        DataSet getTabl(string query)
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source = rtu.db;");
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            cmd.ExecuteNonQuery();
            SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            conn.Dispose();
            return ds;
        }
        public void run()
        {
            excelapp = new Excel.Application();
            excelapp.Visible = true;
            excelapp.SheetsInNewWorkbook = 3; // возвращает или устанавливает количество листов

            /* excelcells = (Excel.Range)excelworksheet.Cells[1, 1];
            excelcells.Value2 = 23;*/
            int i = 0;
            while (form.dataGridViewDTr[0, i].Value != null)
            {
                dtr[i].sec = Convert.ToInt32(form.dataGridViewDTr[0, i].Value);
                dtr[i].x = Convert.ToDouble(form.dataGridViewDTr[1, i].Value);
                dtr[i].y = Convert.ToDouble(form.dataGridViewDTr[2, i].Value);
                i++;
            }
            numberstr = i;
            exceltabl();

        }
        void exceltabl()
        {
            excelapp.Workbooks.Add(Type.Missing);
            excelappworkbooks = excelapp.Workbooks;
            excelappworkbook = excelappworkbooks[1];
            excelsheets = excelappworkbook.Worksheets;
            excelworksheet = (Excel.Worksheet)excelsheets.get_Item(1);

            excelworksheet.PageSetup.PrintTitleRows = "A1"; //Печатаем заголовок на каждой странице
            excelworksheet.PageSetup.RightMargin = 40; //устанавливаем размер левого поля
            excelworksheet.PageSetup.TopMargin = 35; //устанавливаем размер верхнего поля
            excelworksheet.Name = op[0].name;  //устанавливваем имя листа

            excelcells = excelworksheet.Range["A1", Type.Missing];
            excelcells.EntireColumn.ColumnWidth = 1;

            excelcells = excelworksheet.Range["B1", Type.Missing];
            excelcells.EntireColumn.ColumnWidth = 5;

            excelcells = excelworksheet.Range["F1", "J1"];
            excelcells.EntireColumn.ColumnWidth = 10;

            excelcells = excelworksheet.get_Range("A1", "D1");
            excelcells.Merge();
            excelcells.Value2 = form.textBoxName.Text;
            excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
            excelcells.EntireRow.Font.Size = 18;
            excelcells.EntireRow.Font.Bold = true;

            excelcells = excelworksheet.get_Range("F1", Type.Missing);
            excelcells.Merge();
            excelcells.Value2 = op[0].name;
            excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
            excelcells.EntireRow.Font.Size = 18;
            excelcells.EntireRow.Font.Bold = true;

            excelcells = excelworksheet.get_Range("G1", "J1");
            excelcells.Merge();
            excelcells.Value2 = "Угол возвышения: Ɵ=" + form.textBoxTet.Text + "°"; 
            excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
            excelcells.EntireRow.Font.Size = 18;
            excelcells.EntireRow.Font.Bold = true;



            int s = (Convert.ToInt32(form.textBoxD2.Text) - Convert.ToInt32(form.textBoxD1.Text)) / Convert.ToInt32(form.textBoxStep.Text);
            int st = 2; // шаг через который начинать рисовать новую табличку

            int r = 0;
            int b;
            while (form.dataGridViewRls[0, r].Value != null)
            {
                for (int a = 0; a <= s; a++)
                {
                    b = Convert.ToInt32(form.textBoxD1.Text) + a * Convert.ToInt32(form.textBoxStep.Text); // высчитываем направление
                    exceltabtu(st, r, b.ToString());
                    st = st + numberstr + 3;
                }
                b = 0;
                r++;
            }
        }

        void exceltabtu(int a, int rl, string napr)
        {

            excelcells = excelworksheet.get_Range("B" + a.ToString(), "J" + a.ToString());
            excelcells.Merge();
            excelcells.Value2 = rls[rl].name + "    " + napr + "°";
            excelcells.EntireRow.Font.Size = 14;
            excelcells.EntireRow.Font.Bold = true;

            excelcells = excelworksheet.get_Range("B" + (a + 1).ToString(), "B" + (a + 2).ToString());
            excelcells.Merge();
            excelcells.Value2 = "Точки";

            excelcells = excelworksheet.get_Range("C" + (a + 1).ToString(), "E" + (a + 1).ToString());
            excelcells.Merge();
            excelcells.Value2 = "Координаты точек";

            excelcells = excelworksheet.get_Range("C" + (a + 2).ToString(), Type.Missing);
            excelcells.Value2 = "XГ";

            excelcells = excelworksheet.get_Range("D" + (a + 2).ToString(), Type.Missing);
            excelcells.Value2 = "YГ";

            excelcells = excelworksheet.get_Range("E" + (a + 2).ToString(), Type.Missing);
            excelcells.Value2 = "ZГ";

            excelcells = excelworksheet.get_Range("F" + (a + 1).ToString(), "F" + (a + 2).ToString());
            excelcells.Merge();
            excelcells.Value2 = "Наклонная дальность";
            excelcells.WrapText = true;

            excelcells = excelworksheet.get_Range("G" + (a + 1).ToString(), "H" + (a + 1).ToString());
            excelcells.Merge();
            excelcells.Value2 = "Азимут";

            excelcells = excelworksheet.get_Range("I" + (a + 1).ToString(), "J" + (a + 1).ToString());
            excelcells.Merge();
            excelcells.Value2 = "Угол места";

            excelcells = excelworksheet.get_Range("G" + (a + 2).ToString(), Type.Missing);
            excelcells.Value2 = "в градусах";

            excelcells = excelworksheet.get_Range("H" + (a + 2).ToString(), Type.Missing);
            excelcells.Value2 = "в ДУ";

            excelcells = excelworksheet.get_Range("I" + (a + 2).ToString(), Type.Missing);
            excelcells.Value2 = "в градусах";

            excelcells = excelworksheet.get_Range("J" + (a + 2).ToString(), Type.Missing);
            excelcells.Value2 = "в ДУ";

            //форматируем табличку с данными
            excelcells = excelworksheet.get_Range("B" + a.ToString(), "J" + (a + numberstr + 2).ToString());
            excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
            excelcells.VerticalAlignment = Excel.Constants.xlCenter;
            excelcells.Font.Name = "Times New Roman";
            excelcells = excelworksheet.get_Range("B" + (a + 1).ToString(), "J" + (a + numberstr + 2).ToString());
            excelcells.Borders.ColorIndex = 1;
            excelcells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

            //заполняем табличку данными
            for (int i = 0; i < numberstr; i++)
            {
                excelcells = excelworksheet.get_Range("B" + (a + i + 3).ToString(), Type.Missing); // секунда
                excelcells.Value2 = dtr[i].sec;

                excelcells = excelworksheet.get_Range("C" + (a + i + 3).ToString(), Type.Missing); // X топографическая
                excelcells.Value2 = xt(napr, 0, i);

                excelcells = excelworksheet.get_Range("D" + (a + i + 3).ToString(), Type.Missing); // Y топографическая
                excelcells.Value2 = yt(napr, 0, i);

                excelcells = excelworksheet.get_Range("E" + (a + i + 3).ToString(), Type.Missing); // высота
                excelcells.Value2 = zt(0, i);

                double dnak = dnakl(xt(napr, 0, i), yt(napr, 0, i), rl);
                excelcells = excelworksheet.get_Range("F" + (a + i + 3).ToString(), Type.Missing); // наклонная дальность
                excelcells.Value2 = dnak;

                excelcells = excelworksheet.get_Range("G" + (a + i + 3).ToString(), Type.Missing); // азимут в градусах
                excelcells.Value2 = gradus(azimut(xt(napr, 0, i), yt(napr, 0, i), rl));

                excelcells = excelworksheet.get_Range("H" + (a + i + 3).ToString(), Type.Missing); // азимут в ДУ
                excelcells.Value2 = delug(Math.Round(azimut(xt(napr, 0, i), yt(napr, 0, i), rl) * 6000 / 360).ToString());

                excelcells = excelworksheet.get_Range("I" + (a + i + 3).ToString(), Type.Missing); // угол места в градусах
                excelcells.Value2 = gradus(ugmest(xt(napr, 0, i), yt(napr, 0, i), 0, rl, i));

                excelcells = excelworksheet.get_Range("J" + (a + i + 3).ToString(), Type.Missing); // угол места в в делениях угломера
                excelcells.Value = delug(Math.Round(ugmest(xt(napr, 0, i), yt(napr, 0, i), 0, rl, i) * 6000 / 360).ToString());

            }
        }
        double xt(string napr, //направление стрельбы
                        int j, //номер ОП
                        int i) //номер секунды
        {
            double alf = Convert.ToDouble(napr) * Math.PI / 180; // угол стрельбы в III четверти в радианах
            double b = dtr[i].x * Math.Cos(alf);
            return op[j].x + b;
        }
        double yt(string napr, //направление стрельбы
                       int j, //номер ОП
                       int i) //номер секунды
        {
            double alf = Convert.ToDouble(napr) * Math.PI / 180; // угол стрельбы в IV четверти в радианах
            double a = dtr[i].x * Math.Sin(alf);
            return op[j].y + a;
        }
        double zt(int j, //номер ОП
                       int i) //номер секунды
        {
            return op[j].h + dtr[i].y;
        }
        double dnakl(double x, //x топографическая
                        double y, //y топографическая
                        int r) //номер РЛС                         
        {
            double B = Math.Abs(x - rls[r].x);
            double A = Math.Abs(y - rls[r].y);
            double c = Math.Sqrt(B * B + A * A);
            return c;
        }

        //расчет азимута
        double azimut(double x, //x топографическая
                        double y, //y топографическая
                        int r) //номер РЛС                        
        {
            double A = x - rls[r].x;
            double B = y - rls[r].y;
            double C = Math.Sqrt(B * B + A * A);

            if (A < 0 && B > 0) // IV четверть
            {
                return Math.Asin(Math.Abs(A) / C) * 180 / Math.PI + 270;
            }

            if (A < 0 && B < 0) // III четверть
            {
                return Math.Asin(Math.Abs(A) / C) * 180 / Math.PI + 180;
            }

            if (A > 0 && B < 0) // II четверть
            {
                return Math.Asin(Math.Abs(A) / C) * 180 / Math.PI + 90;
            }

            if (A > 0 && B > 0) // I четверть
            {
                return Math.Acos(Math.Abs(A) / C) * 180 / Math.PI;
            }
            return 0;
        }

        //расчет угла места
        double ugmest(double x, //x топографическая
                        double y, //y топографическая
                        int j,  // номер ОП
                        int r,  // номер РЛС
                        int i)  //номер секунды
        {
            double B = Math.Abs(x - rls[r].x);
            double A = Math.Abs(y - rls[r].y);
            double c = Math.Sqrt(B * B + A * A);
            double H = op[j].h - rls[r].h + dtr[i].y;
            double C = Math.Sqrt(c * c + H * H);
            return Math.Asin(H / C) * (180 / Math.PI);
        }

        string gradus(double g)
        {
            string grad = Math.Truncate(g).ToString() + "° ";
            string min = Math.Truncate((g - Math.Truncate(g)) * 60).ToString() + "' ";
            string sec = Math.Round(((g - Math.Truncate(g)) * 60 - Math.Truncate((g - Math.Truncate(g)) * 60)) * 60).ToString() + "''";
            return grad + min + sec;
        }

        string delug(string d)
        {
            string dd = "";
            if (d.Length == 1) dd = "00-0" + d;
            if (d.Length == 2) dd = "00-" + d;
            if (d.Length == 3) dd = d;
            if (d.Length == 4) dd = d.Substring(0, 2) + "-" + d.Substring(2, 2);
            return dd;
        }
    }
}

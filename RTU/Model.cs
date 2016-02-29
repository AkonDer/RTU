using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using ReCoord;

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
        /// количество точек траектории
        /// </summary>
        int numberTr;

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

        RLS[] rls = new RLS[10];
        OP[] op = new OP[10];
        DTr[] dtr = new DTr[10];

        /// <summary>
        /// Конструктор в котором элементы программы заполняются значениями
        /// </summary>
        /// <param name="refer"></param>
        public Model(Form1 refer)  //конструктор
        {
            form = refer;

            DatGridV rlsDG = new DatGridV(form.dataGridViewRls, new string[] { "РЛС", "X", "Y", "H" }, 4, 9, true);
            DatGridV opDG = new DatGridV(form.dataGridViewOp, new string[] { "ОП", "X", "Y", "H" }, 4, 9, true);
            DatGridV datDG = new DatGridV(form.dataGridViewDTr, new string[] { "Секунда", "X", "Y" }, 3, 9, false);

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

        /// <summary>
        /// получение данных ид БД SQLite
        /// </summary>
        /// <param name="query">строка с запросом</param>
        /// <returns>объект DataSet</returns>
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

        /// <summary>
        /// Запуск работы программы
        /// </summary>
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
            numberTr = i;
            exceltabl();

        }

        /// <summary>
        /// Заполняем лист Excel данными
        /// </summary>
        void exceltabl()
        {
            excelapp.Workbooks.Add(Type.Missing);
            excelappworkbooks = excelapp.Workbooks;
            excelappworkbook = excelappworkbooks[1];
            excelsheets = excelappworkbook.Worksheets;

            for (int l = 0; form.dataGridViewOp[0, l].Value != null; l++)
            {

                excelworksheet = (Excel.Worksheet)excelsheets.get_Item(l + 1);

                excelworksheet.PageSetup.PrintTitleRows = "A1"; // Замораживаем строку заголовка на каждой странице
                excelworksheet.PageSetup.RightMargin = 40; //устанавливаем размер левого поля
                excelworksheet.PageSetup.TopMargin = 35; //устанавливаем размер верхнего поля
                excelworksheet.Name = op[l].name;  //устанавливваем имя листа

                //устанавливаем ширину столбцов и параметры шрифта первой строки и отображаем первую строку
                excelcells = excelworksheet.Range["A1", Type.Missing];
                excelcells.EntireColumn.ColumnWidth = 1;

                excelcells = excelworksheet.Range["B1", Type.Missing];
                excelcells.EntireColumn.ColumnWidth = 7;

                excelcells = excelworksheet.Range["E1", "J1"];
                excelcells.EntireColumn.ColumnWidth = 11;

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

                excelcells = excelworksheet.get_Range("G1", "I1");
                excelcells.Merge();
                excelcells.Value2 = "Угол возвышения: Ɵ=" + form.textBoxTet.Text + "°";
                excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                excelcells.EntireRow.Font.Size = 16;
                excelcells.EntireRow.Font.Bold = true;



                int s = (Convert.ToInt32(form.textBoxD2.Text) - Convert.ToInt32(form.textBoxD1.Text)) / Convert.ToInt32(form.textBoxStep.Text);
                int st = 2; // шаг через который начинать рисовать новую табличку

                int b; //направления стрельбы
                for (int r = 0; form.dataGridViewRls[0, r].Value != null; r++)
                {
                    for (int a = 0; a <= s; a++)
                    {
                        b = Convert.ToInt32(form.textBoxD1.Text) + a * Convert.ToInt32(form.textBoxStep.Text); // высчитываем направление
                        excelTabTU(st, r, l, b);
                        st = st + numberTr + 3;
                    }
                    b = 0;
                }
            }
        }
        /// <summary>
        /// Рисуем табличку с точками упреждения
        /// </summary>
        /// <param name="a"></param>
        /// <param name="rl"></param>
        /// <param name="napr"></param>
        void excelTabTU(int a, int rl, int o, double napr)
        {
            //форматируем табличку с данными
            excelcells = excelworksheet.get_Range("B" + a.ToString(), "I" + (a + numberTr + 2).ToString());
            excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
            excelcells.VerticalAlignment = Excel.Constants.xlCenter;
            excelcells.Font.Name = "Times New Roman";
            excelcells = excelworksheet.get_Range("B" + (a + 1).ToString(), "I" + (a + numberTr + 2).ToString());
            excelcells.Borders.ColorIndex = 1;
            excelcells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

            excelcells = excelworksheet.get_Range("B" + a.ToString(), "I" + a.ToString());
            excelcells.Merge();
            excelcells.Value2 = rls[rl].name + "    " + napr.ToString() + "°";
            excelcells.EntireRow.Font.Size = 14;
            excelcells.EntireRow.Font.Bold = true;

            excelcells = excelworksheet.get_Range("B" + (a + 1).ToString(), "B" + (a + 2).ToString());
            excelcells.Merge();
            excelcells.Value2 = "Точки";

            excelcells = excelworksheet.get_Range("C" + (a + 1).ToString(), "D" + (a + 1).ToString());
            excelcells.Merge();
            excelcells.Value2 = "Координаты точек";

            excelcells = excelworksheet.get_Range("C" + (a + 2).ToString(), Type.Missing);
            excelcells.Value2 = "XГ";

            excelcells = excelworksheet.get_Range("D" + (a + 2).ToString(), Type.Missing);
            excelcells.Value2 = "YГ";

            excelcells = excelworksheet.get_Range("E" + (a + 1).ToString(), "E" + (a + 2).ToString());
            excelcells.Merge();
            excelcells.Value2 = "Наклонная дальность";
            excelcells.WrapText = true;

            excelcells = excelworksheet.get_Range("F" + (a + 1).ToString(), "G" + (a + 1).ToString());
            excelcells.Merge();
            excelcells.Value2 = "Азимут";

            excelcells = excelworksheet.get_Range("H" + (a + 1).ToString(), "I" + (a + 1).ToString());
            excelcells.Merge();
            excelcells.Value2 = "Угол места";

            excelcells = excelworksheet.get_Range("F" + (a + 2).ToString(), Type.Missing);
            excelcells.Value2 = "в градусах";

            excelcells = excelworksheet.get_Range("G" + (a + 2).ToString(), Type.Missing);
            excelcells.Value2 = "в ДУ";

            excelcells = excelworksheet.get_Range("H" + (a + 2).ToString(), Type.Missing);
            excelcells.Value2 = "в градусах";

            excelcells = excelworksheet.get_Range("I" + (a + 2).ToString(), Type.Missing);
            excelcells.Value2 = "в ДУ";


            //заполняем табличку значениями
            for (int i = 0; i < numberTr; i++)
            {
                RCoord rc = new RCoord(dtr[i].x, dtr[i].y, op[o].x, op[o].y, op[o].h, rls[rl].x, rls[rl].y, rls[rl].h, napr);

                excelcells = excelworksheet.get_Range("B" + (a + i + 3).ToString(), Type.Missing); // секунда
                excelcells.Value2 = dtr[i].sec;

                excelcells = excelworksheet.get_Range("C" + (a + i + 3).ToString(), Type.Missing); // X топографическая
                excelcells.Value2 = rc.GetXtr;

                excelcells = excelworksheet.get_Range("D" + (a + i + 3).ToString(), Type.Missing); // Y топографическая
                excelcells.Value2 = rc.GetYtr;

                excelcells = excelworksheet.get_Range("E" + (a + i + 3).ToString(), Type.Missing); // наклонная дальность
                excelcells.Value2 = rc.getDnakl;

                excelcells = excelworksheet.get_Range("F" + (a + i + 3).ToString(), Type.Missing); // азимут в градусах
                excelcells.Value2 = rc.ToGrad(rc.GetAlf) + "° " + rc.ToMin(rc.GetAlf) + "' " + rc.ToSec(rc.GetAlf) + "''";

                excelcells = excelworksheet.get_Range("G" + (a + i + 3).ToString(), Type.Missing); // азимут в ДУ
                excelcells.Value2 = rc.ToDU(rc.GetAlf);

                excelcells = excelworksheet.get_Range("H" + (a + i + 3).ToString(), Type.Missing); // угол места в градусах
                excelcells.Value2 = rc.ToGrad(rc.GetUMC) + "° " + rc.ToMin(rc.GetUMC) + "' " + rc.ToSec(rc.GetUMC) + "''";

                excelcells = excelworksheet.get_Range("I" + (a + i + 3).ToString(), Type.Missing); // угол места в в делениях угломера
                excelcells.Value = rc.ToDU(rc.GetUMC);

            }
        }

    }
}

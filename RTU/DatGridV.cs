using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RTU
{
    /// <summary>
    /// Класс определяет интерфейс таблиц
    /// </summary>
    class DatGridV : DataGridView
    {
        public DatGridV(DataGridView dg,
                         string[] sprav, //массив с именами столбцов
                                  int c, //количество столбцов
                                  int r, //количество строк
                                  bool f) //флаг определяет нужена ли колонка с чекбоксом  
        {

            dg.ColumnCount = c; //задаем число столбцов

            if (f == true)
            {
                DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
                column.Name = "Выделить";
                dg.Columns.Insert(c, column);
            }


            dg.RowCount = r + 2; //задаем число строк

            dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // ширина столбцов устанавливается автоматически по ширине элемента управления
            dg.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; //высота строк изменяется в соответствии с содержимым

            //for (int i = 0; i < 5; i++) dg.Rows[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;//устанавливает перенос слов в ячейках
            dg.RowHeadersVisible = false; //отключаем столбец содержащий заголовки строк
                                          //dg.ColumnHeadersVisible = false; //отключаем строку содержащую заголовки столбцов

            dg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // задаем выравнивание по центру в ячейках
            dg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // задаем выравнивание по центру в заголовках столбцов

            // dg.Rows[0].DefaultCellStyle.BackColor = SystemColors.Control; //задаем цвет фона ячеек строки
            dg.Rows[0].Frozen = true;

            dg.AllowUserToAddRows = false;//запрещаем автоматическое добавление строк

            for (int i = 0; i < sprav.Length; i++) dg.Columns[i].HeaderText = sprav[i]; //задаем имена столбцам

            dg.AllowUserToResizeColumns = false; // запрещаем изменение размера строк и столбцов
            dg.AllowUserToResizeRows = false;

            foreach (DataGridViewColumn column in dg.Columns)  // Отменяем сортировку
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


        }

    }
}


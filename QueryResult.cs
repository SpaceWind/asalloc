using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Forms; 
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace ASAlloc
{
    public static class Indexer
    { 
       public static int IndexOf<T>(this IEnumerable<T> source, T value)
        {
            int index = 0;
            var comparer = EqualityComparer<T>.Default; // or pass in as a parameter
            foreach (T item in source)
            {
                if (comparer.Equals(item, value)) return index;
                index++;
            }
            return -1;
        }
    }

    public class QueryResult
    {        
        /////////////////////////////////////////
        public OrderedDictionary getRow(int i)
        {
            OrderedDictionary emptyRow = null;
            if (isRowInRange(i))
                return rows.ElementAt(i);
            return emptyRow;
        }
        public string getValue(int rowIndex, int columnIndex)
        {
            if (isColumnIndexInRange(rowIndex,columnIndex))
            {
                string [] values = new string[rows[rowIndex].Values.Count];
                rows[rowIndex].Values.CopyTo(values,0);
                return values[columnIndex];
            }
            return null;
        }
        public string getValue(int rowIndex, string columnName)
        {
            if (isRowInRange(rowIndex))
            {
                string[] values = new string[rows[rowIndex].Values.Count];
                string[] keys = new string[rows[rowIndex].Keys.Count];
                rows[rowIndex].Values.CopyTo(values, 0);
                rows[rowIndex].Keys.CopyTo(keys, 0);
                return values[Indexer.IndexOf<string>(keys, columnName)];
            }//rows[rowIndex].Values.CopyTo//[columnName];
            else
                return null;
        }
        //-----------------------------------------------------------------
        public void setValue(int rowIndex, int columnIndex, string value)
        {
            if (isColumnIndexInRange(rowIndex, columnIndex))
            {
                string[] values = new string[rows[rowIndex].Values.Count];
                string[] keys =   new string[rows[rowIndex].Keys.Count];
                rows[rowIndex].Values.CopyTo(values,0);
                rows[rowIndex].Keys.CopyTo(keys,0);
                values[columnIndex] = value;

                rows[rowIndex].Clear();
                for (int i = 0; i < values.Count(); i++)
                    rows[rowIndex].Add(keys[i], values[i]);
            }
        }
        public void setValue(int rowIndex, string columnName, string value)
        {
            if (isRowInRange(rowIndex))
                rows[rowIndex][columnName] = value;
        }

        public List<string> getColumn(int i)
        {
            List<string> result = new List<string>();

            if (isColumnIndexInRange(0, i))
                for (int row = 0; row < rows.Count; row++)
                    result.Add(getValue(row, i));
            return result;
        }
        /*
        public List<string> getColumn(string columnName)
        {
            List<string> result = new List<string>();

            if (rows[0].ContainsKey(columnName))
                for (int row = 0; row < rows.Count; row++)
                    result.Add(rows[row][columnName]);
            return result;
        }*/
        public bool rowExist(string columnName, string Value)
        {
            for (int i = 0; i < rows.Count; i++)
                if (rows[i][columnName] == Value)
                    return true;
            return false;
        }
        public int getRowCount()
        {
            return rows.Count;
        }
        public int getColumnCount()
        {
            if (rows.Count != 0)
                return rows[0].Count;
            return 0;
        }
        public string getColumnNameById(int colId)
        {
            return getColumnNameById(0, colId);
        }
        public string getColumnNameById(int row, int colId)
        {
            if (isRowInRange(row))
            {
                string[] values = new string[rows[row].Keys.Count];
                rows[0].Keys.CopyTo(values, 0);
                if (colId < rows[row].Keys.Count)
                    return values[colId];
            }
            return null;
        }
        public void addRow(OrderedDictionary row)
        {
            rows.Add(row);
        }
        public List<OrderedDictionary> toList()
        {
            return rows;
        }

        public void parseColumn(int index, StringDictionary parser)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                string[] values = new string[parser.Values.Count];
                string[] keys   = new string[parser.Keys.Count];
                parser.Keys.CopyTo(keys,0);
                parser.Values.CopyTo(values,0);
                int forEachIterator = 0;
                foreach(string parserString in keys)
                {
                    if (getValue(i, index).ToLower() == parserString.ToLower())
                        setValue(i, index, values[forEachIterator]);
                    forEachIterator++;
                }
            }
        }
        public void parseColumn(int index, string emptyString, string fullString)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                if (rows[i][index] == "")
                    setValue(i, index, emptyString);
                else
                    setValue(i, index, fullString);
            }
        }


        public void addToDataGridView(DataGridView dgv)
        {
            addToDataGridView(dgv, new StringDictionary());
        }
        public void parseColumnNames(StringDictionary dictionary)
        {
            //
        }
        public void addToDataGridView(DataGridView dgv, StringDictionary dictionary)
        {
            if (rows.Count != 0)
            {
                dgv.ColumnCount = rows[0].Count;
                dgv.ColumnHeadersVisible = true;
                for (int colIndex = 0; colIndex < rows[0].Count; colIndex++)
                {
                    string oldName = getColumnNameById(colIndex);
                    if (dictionary.ContainsKey(oldName))
                        dgv.Columns[colIndex].Name = dictionary[oldName];
                    else
                        dgv.Columns[colIndex].Name = oldName;
                    dgv.Columns[colIndex].Width = dgv.Width / rows[0].Count;
                }
                for (int rowIndex = 0; rowIndex < getRowCount(); rowIndex++)
                {
                    string[] row = new string[dgv.ColumnCount];
                    for (int i = 0; i < dgv.ColumnCount; i++)
                        row[i] = getValue(rowIndex, i);
                    dgv.Rows.Add(row);
                }
            }
            else dgv.Rows.Clear();
        }
        public void fromDataGridView(DataGridView dgv, bool onlySelected, bool clearResult)
        {
            if (clearResult)
                rows.Clear();
            var dgvRows = dgv.Rows;
            var selRows = dgv.SelectedRows;
            if (onlySelected)
            {
                for (int i = 0; i < selRows.Count; i++)
                {
                    OrderedDictionary newRow = new OrderedDictionary();
                    for (int j = 0; j < dgv.Columns.Count; j++)
                        newRow[dgv.Columns[j].Name] = selRows[i].Cells[j].Value.ToString();
                    rows.Add(newRow);
                }
            }
            else
            {
                for (int i = 0; i < dgvRows.Count-1; i++)
                {
                    OrderedDictionary newRow = new OrderedDictionary();
                    for (int j = 0; j < dgv.Columns.Count; j++)
                    {
                        string cellValue = (dgvRows[i].Cells[j].Value == null) ? null : dgvRows[i].Cells[j].Value.ToString();
                        newRow[dgv.Columns[j].Name] = cellValue;
                    }
                    rows.Add(newRow);
                }
            }
        }
        ///private methods
        ///
        bool isRowInRange(int i)
        {
            return (i>=0 && i<rows.Count);
        }
        bool isColumnIndexInRange(int row, int column)
        {
            if (!isRowInRange(row))
                return false;
            if (column>=0 && column < rows[row].Count)
                return true;
            return false;
        }
        public bool rowsEquals(OrderedDictionary firstRow, OrderedDictionary secondRow)
        {
            string[] values = new string[firstRow.Values.Count];
            string[] keys = new string[firstRow.Keys.Count];
            firstRow.Keys.CopyTo(keys, 0);
            firstRow.Values.CopyTo(values, 0);
            string[] secondValues = new string[secondRow.Values.Count];
            string[] secondKeys = new string[secondRow.Keys.Count];
            secondRow.Keys.CopyTo(secondKeys, 0);
            secondRow.Values.CopyTo(secondValues, 0);
            if ((values.Count() != secondValues.Count()) || (keys.Count() != secondKeys.Count()))
                return false;
            for (int i = 0; i < values.Count(); i++)
                if ((values[i] != secondValues[i]) || (keys[i] != secondKeys[i]))
                    return false;
            return true;
        }
        public void removeDuplicates()
        {
            for (int iRow = 0; iRow < getRowCount()-1; iRow++)
            {
                for (int cRow = iRow + 1; cRow < getRowCount(); cRow++)
                {
                    if (rowsEquals(rows[iRow], rows[cRow]))
                    {
                        rows.RemoveAt(cRow);
                        cRow--;
                    }
                }
            }
        }

        ///private values
        ///
        private List<OrderedDictionary> rows = new List<OrderedDictionary>();
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cosolem
{
    public partial class frmBusqueda : Form
    {
        private DataTable _DataTable = new DataTable();
        public object _object { get; private set; }

        public frmBusqueda(string text, DataTable _DataTable)
        {
            this.Text = text;
            this._DataTable = _DataTable;
            InitializeComponent();
        }

        private void frmBusqueda_Load(object sender, EventArgs e)
        {
            _object = null;
            dgvResultados.DataSource = _DataTable;
            foreach (DataGridViewColumn _DataGridViewColumn in dgvResultados.Columns)
            {
                if (_DataGridViewColumn.ValueType.Name.ToUpper() == "OBJECT")
                    _DataGridViewColumn.Visible = false;
            }
        }

        private void dgvResultados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _object = dgvResultados.CurrentRow.Cells[dgvResultados.Columns.Count - 1].Value;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void txtFiltroBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable _DataTable = this._DataTable.Clone();
                string columns = null;
                foreach (DataColumn dataColumn in this._DataTable.Columns)
                {
                    if (dataColumn.DataType.Name.ToUpper() != "OBJECT")
                        columns += "[" + dataColumn.ColumnName + "]+";
                }
                columns = columns.Substring(0, columns.Length - 1);
                DataRow[] resultados = this._DataTable.Select(columns + " LIKE '%" + txtFiltroBusqueda.Text.Trim() + "%'");
                if (resultados.Count() > 0) _DataTable = resultados.CopyToDataTable();
                dgvResultados.DataSource = _DataTable;
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }
    }
}

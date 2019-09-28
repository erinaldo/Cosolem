using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace Cosolem
{
    public partial class frmReporteGeneracionContrasena : Form
    {
        private tbPersona _tbPersona = null;

        public frmReporteGeneracionContrasena(tbPersona _tbPersona)
        {
            this._tbPersona = _tbPersona;
            InitializeComponent();
        }

        private void frmReporteGeneracionContrasena_Load(object sender, EventArgs e)
        {
            tbEmpleado _tbEmpleado = _tbPersona.tbEmpleado;
            tbEmpresa _tbEmpresa = _tbEmpleado.tbEmpresa;
            tbCargo _tbCargo = _tbEmpleado.tbCargo;
            tbDepartamento _tbDepartamento = _tbCargo.tbDepartamento;
            tbUsuario _tbUsuario = _tbEmpleado.tbUsuario;

            rptGeneracionContrasena _rptGeneracionContrasena = new rptGeneracionContrasena();
            _rptGeneracionContrasena.razonSocial = _tbEmpresa.razonSocial;
            _rptGeneracionContrasena.nombreCompleto = _tbPersona.nombreCompleto;
            _rptGeneracionContrasena.descripcionCargo = _tbCargo.descripcion;
            _rptGeneracionContrasena.descripcionDepartamento = _tbDepartamento.descripcion;
            _rptGeneracionContrasena.correoElectronico = _tbEmpleado.correoElectronico;
            _rptGeneracionContrasena.nombreUsuario = _tbUsuario.nombreUsuario;
            _rptGeneracionContrasena.contrasena = Util.DesencriptaValor(_tbUsuario.contrasena, _tbUsuario.idUsuario.ToString());
            _rptGeneracionContrasena.numeroIdentificacion = _tbPersona.numeroIdentificacion;
            _rptGeneracionContrasena.usuario = Program.tbUsuario.nombreUsuario;
            _rptGeneracionContrasena.terminal = Program.terminal;

            rvwGeneracionContrasena.LocalReport.DataSources.Clear();
            rvwGeneracionContrasena.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\Seguridad\\rptGeneracionContrasena.rdlc";
            rvwGeneracionContrasena.LocalReport.DataSources.Add(new ReportDataSource("dtsGeneracionContrasena", new List<rptGeneracionContrasena> { _rptGeneracionContrasena }));
            rvwGeneracionContrasena.RefreshReport();
        }
    }
}

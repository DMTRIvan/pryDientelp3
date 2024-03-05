using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryDienteFeliz
{
    class odontologos
    {
        private OleDbConnection conector;
        private OleDbCommand comando;
        private OleDbDataAdapter adaptador;
        private DataTable tabla;

        private int matricula;
        private string odontologo;

        public int Matricula
        {
            get { return matricula; }
            set { matricula = value; }
        }

        public string Odontologo
        {
            get { return odontologo; }
            set { odontologo = value; }
        }

        public odontologos()
        {
            conector = new OleDbConnection(Properties.Settings.Default.CADENA);
            comando = new OleDbCommand();

            comando.Connection = conector;
            comando.CommandType = CommandType.TableDirect;
            comando.CommandText = "Odontologos";

            adaptador = new OleDbDataAdapter(comando);
            tabla = new DataTable();
            adaptador.Fill(tabla);

            DataColumn[] dc = new DataColumn[1];
            dc[0] = tabla.Columns["matricula"];
            tabla.PrimaryKey = dc;
        }

        public void ver(ComboBox combo)
        {
            combo.DisplayMember = "nombre";
            combo.ValueMember = "matricula";
            combo.DataSource = tabla;
        }

        public void buscar()
        {
            DataRow fila = tabla.Rows.Find(matricula);

            if (fila == null)
            {
                odontologo = "";
            }
            else
            {
                matricula = Convert.ToInt32(fila["matricula"]);
                odontologo = fila["odontologo"].ToString();
            }
        }

        public DataTable getOdontologos()
        {
            return tabla;
        }
    }
}

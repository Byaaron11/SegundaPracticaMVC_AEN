using Oracle.ManagedDataAccess.Client;
using SegundaPracticaMVC_AEN.Models;
using System.Data;

namespace SegundaPracticaMVC_AEN.Repositories
{
    #region PROCEDURES ORACLE
    /*
     
    CREATE OR REPLACE PROCEDURE SP_INSERT_COMIC
     (P_NOMBRE NVARCHAR2, P_IMAGEN NVARCHAR2, P_DESCRIPCION NVARCHAR2)
    AS
    BEGIN
      INSERT INTO COMICS VALUES ((SELECT MAX(IDCOMIC) FROM COMICS)+1, P_NOMBRE, P_IMAGEN, P_DESCRIPCION);
      COMMIT;
    END;
     
     */
    #endregion
    public class RepositoryComicOracle: IRepository
    {
        private OracleConnection cn;
        private OracleCommand com;
        private OracleDataAdapter adapter;
        private DataTable tablaComics;

        public RepositoryComicOracle()
        {
            string connectionString =
                "Data Source=LOCALHOST:1521/XE;Persist Security Info=True;User ID=SYSTEM;Password=oracle";
            this.cn = new OracleConnection(connectionString);
            this.com = new OracleCommand();
            this.com.Connection = this.cn;
            string sql = "SELECT * FROM COMICS";
            this.adapter = new OracleDataAdapter(sql, this.cn);
            this.tablaComics = new DataTable();
            adapter.Fill(this.tablaComics);
        }
        public List<Comic> GetComics()
        {
            var consulta = from datos in this.tablaComics.AsEnumerable()
                           select new Comic
                           {
                               IdComic = datos.Field<int>("IDCOMIC"),
                               Nombre = datos.Field<string>("NOMBRE"),
                               Imagen = datos.Field<string>("IMAGEN"),
                               Descripcion = datos.Field<string>("DESCRIPCION")
                           };
            return consulta.ToList();
        }

        public void InsertarComic(string nombre, string imagen, string descripcion)
        {
            OracleParameter pamnombre = new OracleParameter("@NOMBRE", nombre);
            OracleParameter pamimg = new OracleParameter("@IMAGEN", imagen);
            OracleParameter pamdesc = new OracleParameter("@DESCRIPCION", descripcion);
            this.com.Parameters.Add(pamnombre);
            this.com.Parameters.Add(pamimg);
            this.com.Parameters.Add(pamdesc);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_INSERT_COMIC";
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
    }
}

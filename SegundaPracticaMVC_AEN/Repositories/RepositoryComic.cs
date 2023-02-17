using SegundaPracticaMVC_AEN.Models;
using System.Data;
using System.Data.SqlClient;

namespace SegundaPracticaMVC_AEN.Repositories
{
    #region PROCEDURE SQL
    /*
     * CREATE PROCEDURE SP_INSERT_COMIC
        (@NOMBRE NVARCHAR(50), @IMAGEN NVARCHAR(MAX), @DESCRIPCION NVARCHAR(50))
        AS
        INSERT INTO COMICS VALUES ((SELECT MAX(IDCOMIC) FROM COMICS)+1, @NOMBRE, @IMAGEN, @DESCRIPCION)
        GO
     * */
    #endregion
    public class RepositoryComic : IRepository
    {
        private SqlConnection cn;
        private SqlCommand com;
        private SqlDataAdapter adapter;
        private DataTable tablaComics;

        public RepositoryComic()
        {
            string connectionString =
                @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2023";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            string sql = "SELECT * FROM COMICS";
            this.adapter = new SqlDataAdapter(sql, this.cn);
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
            SqlParameter pamnombre = new SqlParameter("@NOMBRE", nombre);
            SqlParameter pamimg = new SqlParameter("@IMAGEN", imagen);
            SqlParameter pamdesc = new SqlParameter("@DESCRIPCION", descripcion);
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

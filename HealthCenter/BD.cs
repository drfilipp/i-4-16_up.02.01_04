using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HealthCenter
{
    class BD
    {
        const string AuthName = "[Authorization]";
        /// <summary>
        /// Экземляр класса SQLConnnection, позволяющая обращаться к базе данных. 
        /// </summary>
        private SqlConnection sql = new SqlConnection(string.Format("Data Source = {0}; Initial Catalog = {1}; Persist Security Info = {2}; integrated security = {3}", "Home", "HealthCenter", "True", "True"));
        /// <summary>
        /// Экземпляр класса DataTable, предназначенный для хранение таблицы БД.
        /// </summary>
        static public DataTable Bibl = new DataTable(), Book = new DataTable(), Chitat_treb = new DataTable(), Client = new DataTable(),
            Cotrud = new DataTable(), Cpes = new DataTable(), Num_Bibl = new DataTable(), Num_Lisen = new DataTable(), Out_Book = new DataTable();
        /// <summary>
        /// Коллекция всех таблиц, удобна в использовании.
        /// </summary>
        static List<DataTable> tables = new List<DataTable>() {
        Bibl, Book, Chitat_treb, Client, Cotrud, Cpes, Num_Bibl, Num_Lisen, Out_Book};
        public enum IdDB
        {
            Bibl, Book, Chitat_treb, Client, Cotrud, Cpes, Num_Bibl, Num_Lisen, Out_Book
        }
        public enum RoleID
        {
            Admin = 1, Specialist, Client
        }

        public BD()
        {
            sql.Open();
        }

        static public bool[] AutCol = new bool[9];

        /// <summary>
        /// Коллекция, в которой хранятся все команды (запросы).
        /// </summary>
        static List<string> commands = new List<string>()
    {"select id_Bibl as 'Номер', Familia as 'Фамилия', [Name] as 'Имя', Otchectvo as 'Отчество', Time_of_work as 'Время работы', Cpes_id as 'Номер специальности', Spesializ as 'Специальность' from [dbo].[Bibl] join [dbo].[Cpes] on [dbo].[Bibl].[Cpes_id] = [dbo].[Cpes].[id_Cpes]",
        "select id_Book as 'Номер', Naim as 'Наименование книги', Author as 'Автор' from [dbo].[Book]",
        " select * from [dbo].Chitat_treb join [dbo].[Cotrud] on [dbo].[Chitat_treb].[Cotrud_id] = [dbo].[Cotrud].[id_Cotrud] " +
        "join [dbo].[Num_Client] on [dbo].[Chitat_treb].[Num_Client_id] = [dbo].[Num_Client].[id_Num_Client] " +
        "join [dbo].[Book] on [dbo].[Chitat_treb].[Book_id] = [dbo].[Book].[id_Book]",
        "select * from [dbo].[client]",
        "select id_Cotrud as 'Номер', Familia as 'Фамилия', [Name] as 'Имя', Otchectvo as 'Отчество' from [dbo].[Cotrud]",
        "select id_Cpes as 'Номер', Spesializ as 'Специальность' from [dbo].[Cpes]",
        " select * from [dbo].[Num_Bibl] join [dbo].[Num_Lisen] on " +
        "[dbo].[Num_Bibl].[Lisen_Num_id] = [dbo].[Num_Lisen].[id_Num_Lisen] " +
        "join [dbo].[Bibl] on [dbo].[Num_Bibl].[Bibl_id] = [dbo].[Bibl].[id_Bibl]",
        "select id_Num_Lisen as 'Номер', Num_Lisen as 'Номер лицензии', Num_sclad as 'Номер склада' from [dbo].[Num_Lisen]",
        " select * from [dbo].[Out_Book] join [dbo].[Num_Bibl] " +
        "on [dbo].[Out_Book].[Bibl_Num_id] = [dbo].[Num_Bibl].[id_Num_Bibl]"
    };
        static List<string> commandsInsert = new List<string>()
    {
        "insert into [dbo].[Bibl]([dbo].[Bibl].[Familia], [dbo].[Bibl].[Name], [dbo].[Bibl].[Otchectvo], [dbo].[Bibl].[Time_of_work], [dbo].[Bibl].[Cpes_id]) values ('{0}', '{1}', '{2}', '{3}', {4})",
        "insert into [dbo].[Book](Naim, Author) values('{0}', '{1}')","","",
        "insert into [dbo].[Cotrud]([Familia], [Name], [Otchectvo]) values ('{0}', '{1}', '{2}' )",
        "insert into [dbo].[Cpes](Spesializ) values ('{0}')", "",
        "insert into [dbo].[Num_Lisen](Num_Lisen, Num_sclad) values ('{0}', '{1}')"
    };

        static List<string> commandsDelete = new List<string>()
    {
        "delete from [dbo].[Bibl] where id_Bibl = {0}", "delete from [dbo].[Book] where id_Book = {0}", "", "",
        "delete from [dbo].[Cotrud] where id_Cotrud = {0}",
        "delete from [dbo].[Cpes] where id_Cpes = {0}", "",
        "delete from [dbo].[Num_Lisen] where id_Num_Lisen = {0}", ""
    };

        static List<string> commandsUpdate = new List<string>()
    {
        "update [dbo].[Bibl] set Familia = '{0}', [Name] = '{1}', Otchectvo = '{2}', Time_of_work = '{3}', Cpes_id = {4} where id_Bibl = {5}",
        "update [dbo].[Book] set Naim = '{0}', Author = '{1}' where id_Book = {2}", "", "",
        "update [dbo].[Cotrud] set Familia = '{0}', [Name] = '{1}', Otchectvo = '{2}' where id_Cotrud = {3}",
        "update [dbo].[Cpes] set Spesializ = '{0}' where id_Cpes = {1}", "",
        "update [dbo].[Num_Lisen] set Num_Lisen = '{0}', Num_sclad = '{1}' where id_Num_Lisen = {2}"
    };

        public void TablesLoad(byte index)
        {
            tables[index].Load((SqlDataReader)new SqlCommand(commands[index], sql).ExecuteReader()); // Загрузка данных в таблицы.
        }

        public DataTable GetFilledTable(byte index)
        {
            DataTable tempDT = new DataTable();
            sql.Open();
            tempDT.Load((SqlDataReader)new SqlCommand(commands[index], sql).ExecuteReader());
            sql.Close();
            return tempDT;
        }

        public void InsertToTable(byte index, string[] inputs)
        {
            sql.Open();
            TablesLoad(index);
            tables[index].Load((SqlDataReader)new SqlCommand(string.Format(commandsInsert[index], inputs), sql).ExecuteReader());
            sql.Close();
        }

        public void DeleteToTable(byte index, uint rowIndex)
        {
            sql.Open();
            TablesLoad(index);
            tables[index].Load((SqlDataReader)new SqlCommand(string.Format(commandsDelete[index], rowIndex), sql).ExecuteReader());
            sql.Close();
        }

        public void UpdateToTable(byte index, string[] inputs)
        {
            sql.Open();
            TablesLoad(index);
            tables[index].Load((SqlDataReader)new SqlCommand(string.Format(commandsUpdate[index], inputs), sql).ExecuteReader());
            sql.Close();
        }
        /// <summary>
        /// Фильтрация таблицы
        /// </summary>
        /// <param name="index">Индекс таблицы</param>
        /// <param name="likeString">Текст поиска</param>
        /// <param name="inputs">Столбцы таблицы</param>
        /// <returns>Отфильтрованная таблица</returns>
        public DataTable FiltherTable(byte index, string likeString, string[] inputs)
        {
            DataTable tempDT = new DataTable();
            sql.Open();
            string com = commands[index] + " where ";
            for (byte i = 0; i < inputs.Length; i++) com += string.Format(" {0} like '%{1}%'", '{' + i.ToString() + '}', likeString) + ((i < inputs.Length - 1) ? " or" : string.Empty);
            tempDT.Load((SqlDataReader)new SqlCommand(string.Format(com, inputs), sql).ExecuteReader());
            sql.Close();
            return tempDT;
        }
        /// <summary>
        /// Сортировка таблицы.
        /// </summary>
        /// <param name="index">Индекс таблицы.</param>
        /// <param name="header">Заголовок столбца.</param>
        /// <param name="e">Событие</param>
        /// <returns></returns>
        //static public DataTable TableSorting(byte index, string header, GridViewSortEventArgs e)
        //{
        //    sql.Open();
        //    DataTable tempDT = new DataTable();
        //    e.SortDirection = (e.SortDirection == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending);
        //    tempDT.Load((SqlDataReader)new SqlCommand(string.Format("{0} order by {1} {2}", commands[index], header, e.SortDirection == SortDirection.Ascending ? "Asc" : "Desc"), sql).ExecuteReader());
        //    sql.Close();
        //    return tempDT;
        //}
        /// <summary>
        /// Авторизация в системе.
        /// </summary>
        /// <param name="Login">Логин</param>
        /// <param name="Password">Пароль</param>
        /// <returns>Тип аккаунта</returns>
        public byte Authorization(string Login, string Password)
        {
            SqlCommand command = new SqlCommand("select [dbo].[Authorization](@Login,@Password)", sql);
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Login", Value = Login });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@Password", Value = Password });
            return byte.Parse(command.ExecuteScalar().ToString());
    }
    /// <summary>
    /// Регистрация нового пользователя.
    /// </summary>
    /// <param name="Login">Логин</param>
    /// <param name="Password">Пароль</param>
    /// <param name="Role_Id">Роль в системе</param>
    /// <param name="System_Access">Доступ к системе (0 - нет, 1 - есть) </param>
    public byte Registration(string Login, string Password, byte Role_Id = (byte)RoleID.Client, string idNumberAbonement = "null", string idSpesialist = "null")
        {
            SqlCommand command = new SqlCommand(
                string.Format("exec [dbo].[Registration] {0}, {1}, {2}, {3}, {4}",Login, Password, Role_Id, 
                idNumberAbonement.Equals(String.Empty) ? "null" : idNumberAbonement, idSpesialist.Equals(string.Empty) ? "null" : idSpesialist), sql);
            return byte.Parse(command.ExecuteScalar().ToString());
        }
    }
}



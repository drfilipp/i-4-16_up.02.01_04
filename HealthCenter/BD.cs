using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HealthCenter
{
    public class BD
    {
        const string AuthName = "[Authorization]";
        /// <summary>
        /// Экземляр класса SQLConnnection, позволяющая обращаться к базе данных. 
        /// </summary>
        public SqlConnection sql = new SqlConnection(string.Format("Data Source = {0}; Initial Catalog = {1}; Persist Security Info = {2}; integrated security = {3}", "Home", "HealthCenter", "True", "True"));
        /// <summary>
        /// Коллекция всех таблиц, удобна в использовании.
        /// </summary>
       
        public enum RoleID
        {
            Admin = 1, Specialist, Client
        }

        public BD()
        {
            sql.Open();
        }

        public enum Tables
        {
            Specialist,
            Preparation
        }

        public enum Views
        {
            Specialist,
            Preparation,
            Provider,
            Visit,
            NumberAbonement,
            Medcard,
            TimeWork,
            Role,
            Auth,
            Special,
            ZakObSuccesHealth
        }

        /// <summary>
        /// Коллекция, в которой хранятся все команды (запросы).
        /// </summary>
        static public List<string> commandsView = new List<string>()
    {"select IdSpecialist as 'Номер', FirstName as 'Имя', Surname as 'Фамилия', Otch as 'Отчество', Birthday as 'День рождения', Number_passport as 'Номер паспорта', Serial_passport as 'Серия паспорта', Special as 'Специальность' from Specialist join Special on Special.IdSpecial = Specialist.IdSpecial ",
        "select IdPreparation as 'Номер', Name_Preparation as 'Наименование', Recipe_Preparation as 'Рецепт' from dbo.Preparation ",
        "select IdProvider as 'Номер', [Provider] as 'Поставщик' from [Provider]",
        "select Visit.IdVisit as 'Номер', Visit.Date_Visit as 'Дата', Visit.Time_Visit as 'Время', NumberAbonement.FirstName as 'Имя', NumberAbonement.Surname as 'Фамилия', NumberAbonement.Otch as 'Отчество', SpecialsView.Специальность as 'Врач' from Visit join NumberAbonement on Visit.IdNumberAbonement = NumberAbonement.IdNumberAbonement join Auth on Auth.IdNumberAbonement = NumberAbonement.IdNumberAbonement join SpecialsView on SpecialsView.Номер = Auth.IdSpecialist",
        "select IdNumberAbonement as 'Номер', FirstName as 'Имя', Surname as 'Фамилия', Otch as 'Отчество', Birthday as 'День рождения', Number_passport as 'Номер паспорта', Serial_passport as 'Серия паспорта' from NumberAbonement",
        "select IdMedcard as 'Номер', Number_medcard as 'Номер медкарты', Date_insert as 'Дата получения'from Medcard ",
        "select IdTimeWork as 'Номер', Start_work as 'Начало работы', End_work as 'Фамилия', IdSpecialist as 'Номер специалиста' from TimeWork ",
        "select IdRole as 'Номер', [Role] as 'Роль' from Role_List ",
        "select IdAuth as 'Номер', [Login] as 'Логин', [Password] as 'Пароль', IdRole as 'Номер роли', IdSpecialist as 'Номер специалиста', IdNumberAbonement as 'Номер абонемента' from Auth",
        "select IdSpecial as 'Номер', Special as 'Специальность' from dbo.[Special]",
        "select IdZakObSuccesHealth as 'Номер', Number_zak as 'Номер заключения', IdVisit as 'Номер посещения', IdTimeWork as 'Номер времени работы', IdMedcard as 'Номер медкарты' from Zakobsucceshealth"
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
        

        public DataTable GetFilledTable(byte index)
        {
            DataTable tempDT = new DataTable();
            tempDT.Load((SqlDataReader)new SqlCommand(commandsView[index], sql).ExecuteReader());
            return tempDT;
        }

        //public void InsertToTable(byte index, string[] inputs)
        //{
        //    sql.Open();
        //    TablesLoad(index);
        //    tables[index].Load((SqlDataReader)new SqlCommand(string.Format(commandsInsert[index], inputs), sql).ExecuteReader());
        //    sql.Close();
        //}

        //public void DeleteToTable(byte index, uint rowIndex)
        //{
        //    sql.Open();
        //    TablesLoad(index);
        //    tables[index].Load((SqlDataReader)new SqlCommand(string.Format(commandsDelete[index], rowIndex), sql).ExecuteReader());
        //    sql.Close();
        //}
        
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
            string com = commandsView[index] + " where ";
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
        public int Authorization(string Login, string Password)
        {
            try
            {
                SqlCommand command = new SqlCommand("select [dbo].[Authorization](@Login,@Password)", sql);
                command.Parameters.Add(new SqlParameter() { ParameterName = "@Login", Value = Login });
                command.Parameters.Add(new SqlParameter() { ParameterName = "@Password", Value = Password });
                return int.Parse(command.ExecuteScalar().ToString());
            }
            catch { return -1; }
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
                string.Format("exec [dbo].[Registration] {0}, {1}, {2}, {3}, {4}", Login, Password, Role_Id,
                idNumberAbonement.Equals(String.Empty) ? "null" : idNumberAbonement, idSpesialist.Equals(string.Empty) ? "null" : idSpesialist), sql);
            return byte.Parse(command.ExecuteScalar().ToString());
        }
        
    }

}



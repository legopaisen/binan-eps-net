using System;
using System.Collections.Generic;
using System.Text;
using Common.DataConnector;

namespace Common.AppSettings
{
    public class UserList
    {
        private List<User> m_lstUsers;

        public UserList()
        {
            m_lstUsers = new List<User>();
            this.GetUserList();
        }

        public List<User> Users
        {
            get { return m_lstUsers; }
        }

        public void GetUserList()
        {
            m_lstUsers.Clear();
            OracleResultSet result = new OracleResultSet();
            result.Query = "SELECT user_code, user_ln, user_fn, user_mi FROM users ORDER BY user_code";
            if (result.Execute())
            {
                while (result.Read())
                {
                    m_lstUsers.Add(new User(result.GetString("user_code").Trim(),
                        result.GetString("user_ln").Trim(), result.GetString("user_fn").Trim(),
                        result.GetString("user_mi").Trim()));
                }
            }

            result.Close();
        }

        //release all tellers list //JVL20100409 merge from mariveles
        public void Release()
        {
            m_lstUsers.Clear();
        }
    }
}

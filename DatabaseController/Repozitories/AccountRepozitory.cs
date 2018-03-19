using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseController.Models;

namespace DatabaseController
{
    public class AccountRepozitory
    {
        public static IEnumerable<Account> GetAll()
        {
            var connection = SingltoneConnection.GetInstance();
            var result = connection.Table<Account>().ToList();
            return result;
        }

        public static Account Get(int id)
        {
            if (id < 0)
            {
                throw new Exception("Неккоректные входные данные в операции Get");
            }
            var connection = SingltoneConnection.GetInstance();
            var res = connection.Table<Account>().FirstOrDefault(o => o.Id == 2);
            if (res == null)
            {
                throw new Exception($"Не найдено записи с id={id}");
            }
            return res;
        }

        public static void Insert(Account newAccount)
        {
            if (newAccount == null)
            {
                throw new Exception("Неккоректные входные данные в операции Insert");
            }
            var connection = SingltoneConnection.GetInstance();

            var result = connection.Insert(newAccount);
            if (result == 0)
            {
                throw new Exception("Ошибка, данные не добавлены в базу");
            }
        }

        public static void Delete(int id)
        {
            if (id < 0)
            {
                throw new Exception("Неккоректные входные данные в операции Delete");
            }
            var connection = SingltoneConnection.GetInstance();
            var rows = connection.Delete<Account>(id);

            if (rows == 0)
            {
                throw new Exception($"Не удалось удалить запись с id = {id}");
            }
        }

        public static void Update(Account account)
        {
            if (account == null)
            {
                throw new Exception("Неккоректные данные в операции Update");
            }
            var connection = SingltoneConnection.GetInstance();
            var result = connection.Update(account, typeof(Account));
            if (result == 0)
            {
                throw new Exception("Ошибка, данные не обновлены");
            }
        }
    }
}
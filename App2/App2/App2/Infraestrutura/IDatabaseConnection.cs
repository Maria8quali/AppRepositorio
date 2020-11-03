
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App2.Infraestrutura
{
    public interface IDatabaseConnection
    {

        SQLiteConnection DbConnection();

    }
}

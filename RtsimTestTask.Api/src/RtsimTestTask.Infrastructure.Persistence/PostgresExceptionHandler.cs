// using Microsoft.EntityFrameworkCore;
// using Npgsql;
//
// namespace RtsimTestTask.Infrastructure.Persistence;
//
// public static class PostgresExceptionHandler
// {
//     public static class ErrorCodes
//     {
//         public const int UniqueViolation = 23505;
//     }
//
//     public static async Task Execute(Func<Task> action)
//     {
//         try
//         {
//             await action();
//         }
//         catch (DbUpdateException e)
//         {
//             if (e.InnerException is not PostgresException psqlEx) throw;
//             switch (psqlEx.SqlState)
//             {
//                 Uniq
//             }
//             Console.WriteLine(e);
//         }
//     }
// }
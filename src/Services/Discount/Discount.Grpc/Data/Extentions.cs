using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public static class Extentions
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
        {
            using var scop = app.ApplicationServices.CreateScope();
            var dbContext = scop.ServiceProvider.GetService<DiscountContext>();
            dbContext?.Database.MigrateAsync();

            return app;
        }
    }
}

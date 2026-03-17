using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            //TODO: GetDiscount from database
            var coupons = await dbContext
                .Coupons
                .FirstOrDefaultAsync(c => c.ProductName == request.ProductName);

            if(coupons == null)
                coupons = new Models.Coupon { ProductName = request.ProductName, Amount = 0, Description = "No Discount" };

            var couponModel = coupons.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();

            if(coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid argument"));

            await dbContext.Coupons.AddAsync(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully CREATED. ProductName: {ProductName}", coupon.ProductName);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();

            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid argument"));

            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully UPDATED. ProductName: {ProductName}", coupon.ProductName);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = dbContext.Coupons.FirstOrDefault(c => c.ProductName == request.ProductName);
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.NotFound, "Discount not found"));
            
            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();
            
            logger.LogInformation("Discount is successfully DELETED. ProductName: {ProductName}", coupon.ProductName);
            var response = new DeleteDiscountResponse { Success = true };
            return response;
        }
    }
}

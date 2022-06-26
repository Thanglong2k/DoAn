using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Enum
{
    /// <summary>
    /// Giới tính
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// Nam
        /// </summary>
        Male = 1,
        /// <summary>
        /// Nữ
        /// </summary>
        Female = 0,
        /// <summary>
        /// Khác
        /// </summary>
        Other = 2
    }
    /// <summary>
    /// Tình trạng làm việc
    /// </summary>
    public enum WorkStatus
    {
        /// <summary>
        /// Đang làm việc
        /// </summary>
        Working = 0,
        /// <summary>
        /// Đã nghỉ việc
        /// </summary>
        Retired = 1,
        /// <summary>
        /// Khác
        /// </summary>
        Other = 2
    }
    /// <summary>
    /// Trạng thái của Object
    /// </summary>
    public enum EntityState
    {
        /// <summary>
        /// Thêm mới
        /// </summary>
        AddNew = 1,
        /// <summary>
        /// Cập nhật
        /// </summary>
        Update = 2,
        /// <summary>
        /// Xóa
        /// </summary>
        Delete = 3
    }
    /// <summary>
    /// Giá sản phẩm
    /// </summary>
    public enum ProductPrices
    {
        All = 0,//Tất cả
        LessThanTwo = 1,//Giá dưới 2tr
        TwoToFour = 2,//Giá 2-4tr
        FourToSeven = 3,//Giá 4-7tr
        SevenToFifteen = 4,//Giá 7-15tr
        OverFifteen = 5,//Giá trên 15tr
    }
    /// <summary>
    /// Pin
    /// </summary>
    public enum Batteries
    {
        All = 0,//Tất cả
        LessThanThree = 1,//Dưới 3000 mah
        ThreeToFive = 2,//3000-5000 mah
        OverFive = 3,//Trên 5000 mah
    }
    /// <summary>
    /// Màn hình
    /// </summary>
    public enum Monitors
    {
        All = 0,//Tất cả
        LessThanSix = 1,//Dưới 6"
        OverSix = 2,//Trên 6"
    }
    /// <summary>
    /// Loại thông báo
    /// </summary>
    public enum Notify
    {
        Comment = 0,//Bình luận
        Evaluation = 1,//Đánh giá
        Order = 2,//Đặt hàng
    }

    public enum OrderStatus
    {
        WaitConfirm = 0,//Chờ xác nhận
        WaitGoods = 1,//Chờ lấy hàng
        Delivering = 2,//Đang giao hàng
        Delivered = 3,//Đã giao hàng
        Cancelled = 4//Đã hủy
    }
    /// <summary>
    /// Trạng thái của Object
    /// </summary>
    public enum MISACode
    {
        /// <summary>
        /// Hợp lệ
        /// </summary>
        Valid = 100,

        /// <summary>
        /// Không hợp lệ
        /// </summary>
        InValid = 200,

        /// <summary>
        /// Thành công  
        /// </summary>
        Success = 900,
    }
}

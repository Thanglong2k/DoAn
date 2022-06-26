//các eum dùng chung
var Enumeration = Enumeration || {};
//Các mode formdetail
Enumeration.FormMode = {
    Add: 1,
    Edit: 2,
    Delete: 3
};
Enumeration.CustomerInfoPopup = {
    Comment: 1,
    Review: 2
};
//Filter Giá
Enumeration.FilterPrice = {
    All : 0, //Tất cả
    LessThanTwo : 1, //Giá dưới 2tr
    TwoToFour : 2, //Giá 2-4tr
    FourToSeven : 3, //Giá 4-7tr
    SevenToFifteen : 4, //Giá 7-15tr
    OverFifteen : 5, //Giá trên 15tr
};
//Filter Pin
Enumeration.FilterBattery = {
    All:0,//Tất cả
        LessThanThree:1,//Dưới 3000 mah
        ThreeToFive:2,//3000-5000 mah
        OverFive:3,//Trên 5000 mah
};
//Filter Monitor
Enumeration.FilterMonitor = {
    All:0,//Tất cả
        LessThanSix:1,//Dưới 6"
        OverSix:2,//Trên 6"
};
//Filter
Enumeration.Filter = {
    Manufacturer:0,//Hãng sản xuất
        Price:1,//Giá
        Monitor:2,//Màn hình
        Battery:3//Pin
};
//Trạng thái thanh toán
Enumeration.PaymentStatus = {
    Paid:true,
    UnPaird:false
};
//Trạng thái đơn hàng
Enumeration.OrderStatus = {
    WaitConfirm:0,//Chờ xác nhận
    WaitGoods:1,//Chờ lấy hàng
    Delivering:2,//Đang giao hàng
    Delivered:3,//Đã giao hàng
    Cancelled:4//Đã hủy
};
//Loại sản phẩm
Enumeration.ProductType = {
    Phone:1,//Sản phẩm
    Accessory:2,//Phụ kiện
    
};
//Giới tính
Enumeration.Gender = {
    Male:false,//Nam
    Female:true,//Nữ
    
};
//Loại màn tra cứu bằng số điện thoại
Enumeration.TypeLookupOrder={
    PurchasedHistory:0,
    FollowOrder:1,
}
//Quyền tài khoản
Enumeration.AccountPermission={
    Admin:1,
    Employee:0,
}
Enumeration.Notify={
    Comment:0,
    Evaluation:1,
    Order:2
}
//Loại areacard
Enumeration.AreaCardType={
    Product:0,
    Post:1
}

export default Enumeration;
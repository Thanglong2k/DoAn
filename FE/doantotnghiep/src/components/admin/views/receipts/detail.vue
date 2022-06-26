<template>
  <div class="receipt-detail-container tw-relative">
    <div class="header-area tw-flex tw-justify-start">
      <div class="back tw-w-8 tw-h-8 tw-mx-6 tw-cursor-pointer" @click="onBack">
        <img class="tw-w-full tw-h-full" :src="ic_Back"/>
      </div>
      <div class="header-text tw-text-gray-700 tw-text-base tw-font-semibold">
        {{
        $t("Receipt.ReceiptDetail")
      }}
      </div>
    </div>
    <div class="receipt-info tw-grid tw-grid-cols-3 tw-grid-rows-1 tw-p-6">
      <div class="receipt-content">
        <div class="receipt"></div>
        <div class="receipt-code">
          <span class="label">Mã đơn hàng</span>
          <span class="tw-ml-6">{{ receiptSuplier.Receipt.ReceiptID }}</span>
        </div>
        <div class="product-quantity">
          <span class="label">Số lượng mặt hàng</span>
          <span class="tw-ml-6">{{ receiptSuplier.Quantity }}</span>
        </div>
        <div class="price-total">
          <span class="label">Tổng giá</span>
          <span class="tw-ml-6">{{ $commonFn.formatMoney(receiptSuplier.Receipt.TotalMoney.toString()) }}</span>
        </div>

        
        
      </div>
      <div class="customer-content">
        <div class="customer-name">
          <span class="label">Tên nhà cung cấp</span>
          <span class="tw-ml-6">{{ receiptSuplier.Suplier.SuplierName }}</span>
        </div>
        <div class="phone-number">
          <span class="label">SĐT nhà cung cấp</span>
          <span class="tw-ml-6">{{ receiptSuplier.Suplier.PhoneNumber }}</span>
        </div>
        <div class="email">
          <span class="label">Email</span>
          <span class="tw-ml-6">{{ receiptSuplier.Suplier.Email }}</span>
        </div>
        <div class="address">
          <span class="label">Địa chỉ</span>
          <span class="tw-ml-6">{{ receiptSuplier.Receipt.Address }}</span>
        </div>
        
      </div>
      <div class="status">
        <div class="payment-status">
          <span class="label">Trạng thái thanh toán</span>
          <span class="tw-ml-6">{{
            receiptSuplier.Receipt.PaymentStatus
              ? "Đã thanh toán"
              : "Chưa thanh toán"
          }}</span>
        </div>
        <div class="receipt-date">
          <span class="label">Ngày đặt hàng</span>
          <span class="tw-ml-6">{{ receiptSuplier.Receipt.CreatedDate }}</span>
        </div>
      </div>
    </div>
    <div
      class="receipt-detail-list tw-absolute tw-bottom-0 tw-w-full tw-p-6"
      style="height: calc(100% - 115px - 49px - 17px)"
    >
      <el-table
        class="table"
        breceipt
        :data="receiptDetails"
        height="calc(100% - 49px - 16px)"
        style="width: 100%"
      >
        <el-table-column type="index" width="50" label="STT"> </el-table-column>
        <el-table-column
          prop="ReceiptDetail.ReceiptDetailID"
          label="Mã mặt hàng"
          min-width="150"
        >
        </el-table-column>
        <el-table-column
          prop="Product.ProductName"
          label="Tên mặt hàng"
          min-width="200"
        >
        </el-table-column>
        <el-table-column prop="Product.Image" label="Ảnh" width="100"  align="center">
          <template slot-scope="scope">
            <div class="image tw-w-20 tw-h-20">
              <el-image :src="scope.row.Product.Avatar" fit="contain">
                <div slot="placeholder" class="image-slot">
                  {{ $t("Common.Loading") }}<span class="dot">...</span>
                </div>
              </el-image>
            </div>
          </template>
        </el-table-column>
        <el-table-column
          prop="ReceiptDetail.Quantity"
          label="Số lượng"
          width="150"
          align="right"
        >
        </el-table-column>
        <el-table-column
          prop="ReceiptDetail.UnitPrice"
          label="Đơn giá"
          width="200"
          align="right"
        >
        <template slot-scope="scope">
              {{$commonFn.formatMoney2(scope.row.ReceiptDetail.UnitPrice.toString())}}
            </template>
        </el-table-column>
        <el-table-column
          prop="ProductAttribute.Memory"
          label="Dung lượng"
          width="150"
           align="right"
        >
        </el-table-column>
        <el-table-column label="Thành tiên" width="200" align="right">
          <template slot-scope="scope">
              {{$commonFn.formatMoney2((scope.row.ReceiptDetail.Quantity * scope.row.ReceiptDetail.UnitPrice).toString())}}
            </template>
        </el-table-column>
      </el-table>
      <el-pagination
        class="tw-flex tw-justify-end tw-h-12 tw-items-center"
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange"
        :current-page.sync="pageIndex"
        :page-size="pageSize"
        :page-sizes="pageSizes"
        layout="total,sizes, prev, pager, next"
        :total="receiptDetailTotal"
      >
      </el-pagination>
    </div>
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";
export default {
  data() {
    return {
      pageSizes: [5, 10, 15],
      pageSize: 5,
      pageIndex: 1,
      active: 0,
      ic_Back:require("@/assets/icons/header/back.png")
    };
  },
  computed: {
    ...mapState("receipt", [
      "receiptDetails",
      "receiptDetailTotal",
      "pageReceiptDetailTotal",
      "receiptSuplier",
    ]),
  },
  async created() {
    const me = this;
    await me.getReceiptDetail(me.pageIndex, me.pageSize);
    await me.getSuplierReceipt({ receiptID: me.$route.query.id });
    me.active=me.receiptSuplier.Receipt.ReceiptStatus
  },
  methods: {
    ...mapActions("receipt", ["detailReceipt", "getSuplierReceipt","updateReceipt"]),
    onBack(){
      const me=this
      me.$router.push('/admin/receipt')
    },
    getReceiptDetail(pageIndex, pageSize) {
      const me = this;
      const params = {
        receiptID: me.$route.query.id,
        pageIndex: pageIndex,
        pageSize: pageSize,
      };
      me.detailReceipt(params);
    },
    handleSizeChange(val) {
      const me = this;
      me.pageSize = val;
      me.getReceiptDetail(me.pageIndex, me.pageSize);
    },
    handleCurrentChange(val) {
      const me = this;
      me.pageIndex = val;
      me.getReceiptDetail(me.pageIndex, me.pageSize);
    },
    getReceiptStatus(receiptStatus) {
      const me = this;
      
      const enumReceiptStatus = me.$enumeration.ReceiptStatus;
      switch (receiptStatus) {
        case enumReceiptStatus.WaitConfirm:
          return "Chờ xác nhận";
        case enumReceiptStatus.WaitGoods:
          return "Chờ lấy hàng";
        case enumReceiptStatus.Delivering:
          return "Đang giao hàng";
        case enumReceiptStatus.Delivered:
          return "Đã giao hàng";
        case enumReceiptStatus.Cancelled:
          return "Đã hủy";
        default:
          return "abc";
      }
    },
    setActive(index) {
      const me = this;
      me.active = index;
      alert(index);
    },
    nextStep() {
      const me = this;
      
      me.active++;
      const newReceipt=JSON.parse(JSON.stringify(me.receiptSuplier.Receipt))
      newReceipt.ReceiptStatus=me.active
      const params={
        id:newReceipt.ReceiptID,
        params:newReceipt
      }
      me.updateReceipt(params)
    },
  },
};
</script>

<style scoped>
.label {
  font-size: 14px;
  font-weight: 600;
}
.receipt-info{
  line-height: 30px;
}
</style>
<style lang="scss" scoped>
::v-deep{
  .el-steps{
  }
  .el-button{
    display: flex;
    margin-top: 12px;
    width: 80px;
    height: 40px;
    justify-content: center;
  }
}
</style>
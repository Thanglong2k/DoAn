<template>
  <div class="order-detail-container tw-relative">
    <div class="header-area tw-flex tw-justify-start">
      <div class="back tw-w-8 tw-h-8 tw-mx-6 tw-cursor-pointer" @click="onBack">
        <img class="tw-w-full tw-h-full" :src="ic_Back"/>
      </div>
      <div class="header-text tw-text-gray-700 tw-text-base tw-font-semibold">
        {{
        $t("Order.OrderDetail")
      }}
      </div>
    </div>
    <div class="order-info tw-grid tw-grid-cols-3 tw-grid-rows-1 tw-p-6">
      <div class="order-content">
        <div class="order"></div>
        <div class="order-code">
          <span class="label">Mã đơn hàng</span>
          <span class="tw-ml-6">{{ orderCustomer?orderCustomer.Order.OrderID:'' }}</span>
        </div>
        <div class="product-quantity">
          <span class="label">Số lượng mặt hàng</span>
          <span class="tw-ml-6">{{ orderCustomer?orderCustomer.Quantity:'' }}</span>
        </div>
        <div class="price-total">
          <span class="label">Tổng giá</span>
          
          <span class="tw-ml-6">{{ $commonFn.formatMoney(orderCustomer?orderCustomer.Order.TotalPrice.toString():'') }}</span>
        </div>
      </div>
      <div class="customer-content">
        <div class="customer-name">
          <span class="label">Tên khách hàng</span>
          <span class="tw-ml-6">{{ orderCustomer?orderCustomer.Customer.CustomerName:'' }}</span>
        </div>
        <div class="phone-number">
          <span class="label">SĐT khách hàng</span>
          <span class="tw-ml-6">{{ orderCustomer?orderCustomer.Customer.PhoneNumber:'' }}</span>
        </div>
        <div class="email">
          <span class="label">Email</span>
          <span class="tw-ml-6">{{ orderCustomer?orderCustomer.Customer.Email:'' }}</span>
        </div>
        <div class="address">
          <span class="label">Địa chỉ nhận hàng</span>
          <span class="tw-ml-6">{{ orderCustomer?orderCustomer.Order.DeliveryAddress:'' }}</span>
        </div>
      </div>
      <div class="status">
        <div class="payment-status">
          <span class="label">Trạng thái thanh toán</span>
          <span class="tw-ml-6">{{
            orderCustomer?(orderCustomer.Order.PaymentStatus
              ? "Đã thanh toán"
              : "Chưa thanh toán"):''
          }}</span>
        </div>
        <div class="order-date">
          <span class="label">Ngày đặt hàng</span>
          <span class="tw-ml-6">{{ orderCustomer?orderCustomer.Order.CreatedDate:'' }}</span>
        </div>
        <div class="order-status">
          <span class="label">Trạng thái đơn hàng</span>
          <span class="tw-ml-6">{{
            getOrderStatus(orderCustomer?orderCustomer.Order.OrderStatus:'')
          }}</span>
          <div class="order-status-action tw-flex tw-mt-4 tw-w-full">
            <span class="tw-relative tw--left-28">
              <el-steps
                :active="active"
                process-status="finish"
                :finish-status="finishStatus"
                align-center
              >
                <el-step
                  v-for="index in 4"
                  :key="index"
                  @click="setActive(index - 1)"
                  :title="getOrderStatus(index - 1)"
                ></el-step>
              </el-steps>
            </span>
            <el-button style="margin-top: 12px" @click="nextStep"
            class="next-btn"
            v-if="orderCustomer&&orderCustomer.Order.OrderStatus<$enumeration.OrderStatus.Delivered"
              >Cập nhật</el-button
            >
            <el-button style="margin-top: 12px" @click="cancelOrder"
            class="cancel-btn"
            v-if="orderCustomer&&orderCustomer.Order.OrderStatus<$enumeration.OrderStatus.Delivered"
              >Hủy đơn</el-button
            >
          </div>
        </div>
      </div>
    </div>
    <div
      class="order-detail-list tw-absolute tw-bottom-0 tw-w-full tw-p-6"
      style="height: calc(100% - 170px - 49px - 17px)"
    >
      <el-table
        class="table"
        border
        :data="orderDetails"
        height="calc(100% - 49px - 16px)"
        style="width: 100%"
      >
        <el-table-column type="index" width="50" label="STT"> </el-table-column>
        <el-table-column
          prop="OrderDetail.OrderDetailID"
          label="Mã mặt hàng"
          width="150"
        >
        </el-table-column>
        <el-table-column
          prop="Product.ProductName"
          label="Tên mặt hàng"
          min-width="200"
        >
        </el-table-column>
        <el-table-column prop="Product.Avatar" label="Ảnh" width="200">
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
          prop="OrderDetail.Quantity"
          label="Số lượng"
          width="150"
        >
        </el-table-column>
        <el-table-column
          prop="OrderDetail.UnitPrice"
          label="Đơn giá"
          width="200"
        >
        <template slot-scope="scope">
              {{$commonFn.formatMoney2(scope.row.OrderDetail.UnitPrice.toString())}}
            </template>
        </el-table-column>
        <el-table-column
          prop="ProductAttribute.Memory"
          label="Dung lượng"
          width="150"
        >
        </el-table-column>
        <el-table-column label="Thành tiên" width="200">
          <template slot-scope="scope">
              {{$commonFn.formatMoney2((scope.row.OrderDetail.Quantity * scope.row.OrderDetail.UnitPrice).toString())}}
            </template>
          
        </el-table-column>
        <el-table-column label="IMEI" width="200">
          <template  slot-scope="scope">
              <div @click="updateIMEI(scope.row.OrderDetail)">Cập nhật IEMI</div>
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
        :total="orderDetailTotal"
      >
      </el-pagination>
    </div>
    <ProductDetail ref="popup" v-if="showDetail" :orderDetail="orderDetail" @closePopup="closePopup" @updateIMEIValue="updateIMEIValue"/>
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";
import ProductDetail from '../products/product-detail.vue';
export default {
  components:{
    ProductDetail
  },
  data() {
    return {
      pageSizes: [5, 10, 15],
      pageSize: 5,
      pageIndex: 1,
      active: 1,
      ic_Back:require("@/assets/icons/header/back.png"),
      orderDetail:null,
      showDetail:false
    };
  },
  computed: {
    ...mapState("order", [
      "orderDetails",
      "orderDetailTotal",
      "pageOrderDetailTotal",
      "orderCustomer",
    ]),
    finishStatus(){
      const me=this
      debugger
      return me.active>4||me.active===0?"error":"success"
    }
  },
   async mounted() {
    const me = this;
      

      await me.getOrderDetail(me.pageIndex, me.pageSize);
      await me.getCustomerOrder({ orderID: me.$route.query.id });




      me.active = me.orderCustomer.Order.OrderStatus+1;

    

  },
  methods: {
    ...mapActions("order", ["detailOrder", "getCustomerOrder", "updateOrder","updateIMEIByOrderDetailID"]),
    onBack(){
      const me=this
      me.$router.push('/admin/order')
    },
    getOrderDetail(pageIndex, pageSize) {
      const me = this;
      const params = {
        orderID: me.$route.query.id,
        pageIndex: pageIndex,
        pageSize: pageSize,
      };
      me.detailOrder(params);
    },
    handleSizeChange(val) {
      const me = this;
      me.pageSize = val;
      me.getOrderDetail(me.pageIndex, me.pageSize);
    },
    handleCurrentChange(val) {
      const me = this;
      me.pageIndex = val;
      me.getOrderDetail(me.pageIndex, me.pageSize);
    },
    getOrderStatus(orderStatus) {
      const me = this;
      const enumOrderStatus = me.$enumeration.OrderStatus;
      switch (orderStatus) {
        case enumOrderStatus.WaitConfirm:
          return "Chờ xác nhận";
        case enumOrderStatus.WaitGoods:
          return "Chờ lấy hàng";
        case enumOrderStatus.Delivering:
          return "Đang giao hàng";
        case enumOrderStatus.Delivered:
          return "Đã giao hàng";
        case enumOrderStatus.Cancelled:
          return "Đã hủy";
          default: return ''
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
      const newOrder = JSON.parse(JSON.stringify(me.orderCustomer.Order));
      newOrder.OrderStatus = me.active-1;
      const params = {
        id: newOrder.OrderID,
        params: newOrder,
      };
      const h = this.$createElement;
      me.updateOrder(params)
        .then(async (res) => {
          if (res) {
            me.$notify({
              title: me.$t("Notify.Success"),
              message: h(
                "i",
                { style: "color: teal" },
                me.$t("Notify.UpdateSuccess", { object: "đơn hàng" })
              ),
              type: "success",
            });
            await me.getOrderDetail(me.pageIndex, me.pageSize);
            await me.getCustomerOrder({ orderID: me.$route.query.id });
          } else {
            me.$notify({
              title: me.$t("Notify.Error"),
              message: h(
                "i",
                { style: "color: teal" },
                me.$t("Notify.HasError")
              ),
              type: "error",
            });
          }
        })
        .catch((e) => {
          me.$notify({
            title: me.$t("Notify.Error"),
            message: h("i", { style: "color: teal" }, me.$t("Notify.HasError")),
            type: "error",
          });
        });
    },
    cancelOrder() {
      const me = this;

      me.active=5;
      const newOrder = JSON.parse(JSON.stringify(me.orderCustomer.Order));
      newOrder.OrderStatus = me.$enumeration.OrderStatus.Cancelled;
      const params = {
        id: newOrder.OrderID,
        params: newOrder,
      };
      const h = this.$createElement;
      me.updateOrder(params)
        .then(async (res) => {
          if (res) {
            me.$notify({
              title: me.$t("Notify.Success"),
              message: h(
                "i",
                { style: "color: teal" },
                me.$t("Notify.UpdateSuccess", { object: "đơn hàng" })
              ),
              type: "success",
            });
            await me.getOrderDetail(me.pageIndex, me.pageSize);
            await me.getCustomerOrder({ orderID: me.$route.query.id });
          } else {
            me.$notify({
              title: me.$t("Notify.Error"),
              message: h(
                "i",
                { style: "color: teal" },
                me.$t("Notify.HasError")
              ),
              type: "error",
            });
          }
        })
        .catch((e) => {
          me.$notify({
            title: me.$t("Notify.Error"),
            message: h("i", { style: "color: teal" }, me.$t("Notify.HasError")),
            type: "error",
          });
        });
    },
    updateIMEI(orderDetail){
      const me=this
      me.orderDetail=orderDetail
      me.showDetail=true
      me.$nextTick(()=>{

        me.$refs.popup.setModel(true)
      })
    },
    closePopup(){
      const me=this
      me.showDetail=false
    },
    updateIMEIValue(imeis,orderDetailID){
      const me=this
      const params={
        imeis:imeis.join(','),
        orderDetailID:orderDetailID
      }
      me.updateIMEIByOrderDetailID(params).then(res=>{
        me.getOrderDetail(me.pageIndex,me.pageSize)
      })
    }
  },
};
</script>

<style scoped>
.label {
  font-size: 14px;
  font-weight: 600;
}
.order-info {
  line-height: 30px;
}
.next-btn{
  @apply tw-bg-green-500 tw-text-white !important
}
.cancel-btn{
  @apply tw-bg-red-500 tw-text-white !important
}
</style>
<style lang="scss" scoped>
::v-deep {
  .el-steps {
  }
  .el-button {
    display: flex;
    margin-top: 12px;
    width: 80px;
    height: 40px;
    justify-content: center;
  }
}
</style>
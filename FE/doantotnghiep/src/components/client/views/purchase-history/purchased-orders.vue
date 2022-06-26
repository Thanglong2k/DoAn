<template>
  <div class="purchased-orders tw-bg-white" v-if="customer">
    <div class="header">
      Chào {{ customer.Gender === $enumeration.Gender.Male ? "anh" : "chị" }}
      {{ customer.CustomerName }}, {{ customer.PhoneNumber }}
    </div>
    <div class="order-status tw-flex" v-if="type===$enumeration.TypeLookupOrder.FollowOrder">
      <el-button
        v-for="(tab, index) in tabs"
        :key="index"
        :class="{ 'is-active': activeTab === index - 1 }"
        @click="selectStatus(index-1)"
        >{{ tab }}</el-button
      >
    </div>
    <div class="order-list" v-if="purchasedHistory.length>0">
      <div class="order-header tw-bg-gray-100 tw-flex">
        <div class="order-column border-right tw-p-4 tw-w-14">STT</div>
        <div class="order-column tw-flex-1 tw-p-4 tw-text-center">Đơn hàng</div>
      </div>
      <div
        class="order-items"
        v-for="(item, index) in purchasedHistory"
        :key="index"
      >
        <div class="order-row tw-flex">
          <div class="order-column border-right tw-p-4 tw-w-14">
            {{ index + 1 }}
          </div>
          <div
            class="order-column border-right tw-p-4 tw-flex-1 tw-text-center"
          >
            <div
              class="product tw-py-2 tw-flex"
              v-for="(orderDetail, index1) in item.OrderDetails"
              :key="index1"
            >
              <div class="name tw-flex-1">
                {{
                  orderDetail.Product.ProductName +
                  " " +
                  orderDetail.ProductAttribute.Memory +
                  "GB"
                }}
              </div>
              <div class="image tw-w-32">
                <img :src="orderDetail.Product.Avatar" />
              </div>
              <div class="quantity tw-w-40">
                Số lượng : {{ orderDetail.OrderDetail.Quantity }}
              </div>
              <div class="price tw-w-56">
                Giá :
                {{
                  $commonFn.formatMoney2(
                    orderDetail.OrderDetail.UnitPrice.toString()
                  )
                }}
              </div>
            </div>
          </div>
          <div class="order-column border-right tw-w-32 tw-text-center tw-p-4">
            Tổng tiền:
            {{ $commonFn.formatMoney2(item.Order.TotalPrice.toString()) }}
          </div>
          <div class="order-column tw-w-32 tw-text-center tw-p-4" v-if="activeTab===$enumeration.OrderStatus.WaitConfirm || activeTab===-1">
            <el-button class="cacel-btn" @click="onCancelOrder(item.Order)" v-if="item.Order.OrderStatus===$enumeration.OrderStatus.WaitConfirm">Hủy đơn</el-button>
          </div>
        </div>
        <div class="line"></div>
      </div>
    </div>
    <div class="empty-order tw-p-6 tw-text-center" v-else>
      Không có đơn hàng nào
    </div>
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";
export default {
props:{
    type:{
      type:Number,
      default:0
    }
  },
  data() {
    return {
      activeTab: -1,
      tabs: [
        "Tất cả",
        "Chờ xác nhận",
        "Chờ lấy hàng",
        "Đang giao hàng",
        "Thành công",
        "Đã hủy",
      ],
    };
  },
  computed: {
    ...mapState("customer", ["customer"]),
    ...mapState("order", ["purchasedHistory",'phoneNumber']),
  },
  methods:{
    ...mapActions('order',['getFollowOrder','updateOrder']),
    selectStatus(status){
      const me=this
      me.activeTab=status
      me.getFollowOrder({phoneNumber:me.phoneNumber,status:status})
    },
    onCancelOrder(order){
      const me=this
      const newOrder = JSON.parse(JSON.stringify(order));
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
              dangerouslyUseHTMLString: true,
              message: h(
                "i",
                { style: "color: teal" },
                me.$t("Notify.CancelOrderSuccess", { orderID: newOrder.OrderID })
                
              ),
              type: "success"
            });
            await me.getFollowOrder({phoneNumber:me.phoneNumber,status:me.activeTab})
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
    }
  }
};
</script>

<style lang="scss" scoped>
.line {
  height: 1px;
  width: 100%;
  background-color: rgb(175, 174, 174);
}

.border-right {
  border-right: 1px solid rgb(175, 174, 174);
}
</style>
<style scoped>
.is-active {
  @apply tw-bg-red-500 tw-text-white !important;
}
.cacel-btn{
  @apply tw-bg-red-500 tw-text-white !important;
}
</style>
<template>
  <el-dialog
    ref="dialog"
    title="Đặt hàng thành công"
    :visible.sync="modelDialog"
    @close="closePopup"
  >
    <template #default>
      <div class="sucess-notify tw-flex tw-flex-col tw-items-center">
        <img class="tw-h-10" :src="imgSuccess" />
        <div class="thank">Cảm ơn quý khách đã mua hàng tại TTL Store</div>
        <div class="contact tw-text-center">
          Tổng đài viên TTL Store sẽ liên hệ đến quý khách trong vòng 5 phút.<br/>
          Xin cảm ơn quý khách đã cho chúng tôi cơ hội được phục vụ!
        </div>
      </div>
      <div class="customer-info tw-mt-2">
        <table class="tw-w-full">
          <thead class="tw-bg-gray-200">
            <tr>
              <th colspan="2">Thông tin khách hàng</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>Họ và tên</td>
              <td>{{ customer.CustomerName }}</td>
            </tr>
            <tr>
              <td>Số điện thoại</td>
              <td>{{ customer.PhoneNumber }}</td>
            </tr>
            <tr>
              <td>Email</td>
              <td>{{ customer.Email }}</td>
            </tr>
          </tbody>
        </table>
      </div>
      <div class="order-info tw-mt-2">
        <table class="tw-w-full">
          <thead class="tw-bg-gray-200">
            <tr>
              <th colspan="2">Thông tin đơn hàng</th>
              <th>Số lượng</th>
              <th>Đơn giá</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(item, index) in product" :key="index">
              <td>
                <div class="image tw-w-20 tw-h-20">
                  <el-image :src="item.Avatar" fit="contain">
                    <div slot="placeholder" class="image-slot">
                      Loading<span class="dot">...</span>
                    </div>
                  </el-image>
                </div>
              </td>
              <td>{{ item.ProductName + " " + item.ProductAttributes[0].Memory+"GB"}}</td>
              <td>{{item.ProductAttributes[0].Quantity}}</td>
              <td>
                <div class="promotion-price tw-text-red-500 tw-text-base" v-if="item.ProductAttributes[0].PromotionPrice">{{$commonFn.formatMoney2(item.ProductAttributes[0].PromotionPrice.toString())}}</div>
                <div class="price"
                  :class="[{'tw-text-red-500 tw-text-base':!item.ProductAttributes[0].PromotionPrice},
                  {'tw-line-through tw-text-xs':item.ProductAttributes[0].PromotionPrice}]"
                >{{$commonFn.formatMoney2(item.ProductAttributes[0].Price.toString())}}</div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </template>
    <template #footer>
      <el-button @click="backHome" class="back-home-btn">{{
        $t("Button.BackHome")
      }}</el-button>
    </template>
  </el-dialog>
</template>

<script>
export default {
  props: {
    customer: {
      type: Object,
      default: () => {},
    },
    productCart: {
      type: Array,
      default: () => {},
    },
  },
  data() {
    return {
      modelDialog: false,
      imgSuccess: require("@/assets/images/clients/common/success.png"),
    };
  },
  computed:{
    //...mapState("cart", ["productCart", "productCartLength"]),
    product(){
      return this.productCart
    }
  },
  methods: {
    setModel(value) {
      const me = this;
      me.modelDialog = value;
    },
    backHome() {
      const me = this;
      me.$router.push("/");
      me.modelDialog = false;
      me.closePopup()
    },
    closePopup(){
      const me=this
      me.$emit("closePopup")
    }
  },
};
</script>

<style scoped>
.back-home-btn {
  @apply tw-bg-red-500 tw-text-white !important;
}
</style>
<style lang="scss" scoped>
::v-deep{
    .el-dialog__body{
        padding-top:24px;
    }
}
</style>
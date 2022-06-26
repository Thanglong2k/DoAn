<template>
  <div
    class="
      warranty-lookup-container
      tw-bg-white tw-p-6 tw-flex tw-justify-center
      tw-h-full
    "
  >
    <div class="warranty-area tw-w-3/4 tw-flex tw-flex-col tw-items-center">
      <div class="header-area tw-text-2xl tw-text-gray-600">
        Tra cứu bảo hành
      </div>
      <div class="search-area tw-mt-6">
        <span>Nhập IMEI sản phẩm</span>
        <el-input
          class="tw-my-4"
          v-model="imei"
          placeholder="Nhập IMEI điện thoại của bạn"
        ></el-input>
        <div class="button-area tw-flex tw-justify-center">
          <el-button @click="onWarrentyLookup">Tra cứu</el-button>
        </div>
      </div>
      <div class="result-area tw-mt-6" v-if="product">
        <div class="result tw-text-2xl tw-text-gray-600">Kết quả</div>
        <table class="tw-mt-6">
          <tr>
            <td>Tên khách hàng</td>
            <td>{{ product.Customer.CustomerName }}</td>
          </tr>
          <tr>
            <td>Số điện thoại</td>
            <td>{{ product.Customer.PhoneNumber }}</td>
          </tr>
          <tr>
            <td>Tên sản phẩm</td>
            <td>
              {{
                product.Product.ProductName +
                " " +
                product.ProductAttribute.Memory +
                "GB"
              }}
            </td>
          </tr>
          <tr>
            <td>Ảnh sản phẩm</td>
            <td><img style="width:200px; height:200px;" :src="product.Product.Avatar" /></td>
          </tr>
          <tr>
            <td>Thời gian bảo hành</td>
            <td>{{ product.Product.Warranty+" tháng" }}</td>
          </tr>
          <tr>
            <td>Ngày mua</td>
            <td>{{ $commonFn.formatDate(new Date(product.Order.CreatedDate),'dd/MM/yyyy') }}</td>
          </tr>
          <tr>
            <td>Thời hạn bảo hành còn lại</td>
            <td>
              {{
                remainingWarrentyPeriod > 0
                  ? (Math.ceil(remainingWarrentyPeriod)>=1?Math.ceil(remainingWarrentyPeriod) + " tháng":remainingWarrentyPeriod + " ngày")
                  : "Hết bảo hành"
              }}
            </td>
          </tr>
        </table>
      </div>
    </div>
  </div>
</template>

<script>
import { mapActions, mapState } from "vuex";
export default {
  data() {
    return {
      imei: "",
    };
  },
  computed: {
    ...mapState("product", ["product"]),
    remainingWarrentyPeriod() {

      return this.product.Product.Warranty-(
        (new Date() - new Date(this.product.Order.CreatedDate)) /
        1000 /
        60 /
        60 /
        24 /
        30
      );
    },
  },
  methods: {
    ...mapActions("product", ["warrentyLookup"]),
    async onWarrentyLookup() {
      const me = this;
      const h = this.$createElement;
      if(me.imei===''){
        me.$notify.warning({
          title: me.$t("Notify.Warning"),
          message: h(
            "i",
            { style: "color: teal" },
            me.$t("Notify.WarningIMEIInsert")
          ),
        });
      }else{
        me.$commonFn.showMark("warranty-lookup-container")
        await me.warrentyLookup({ imei: me.imei });
        me.$commonFn.hideMark("warranty-lookup-container")
        if(!me.product){
          me.$notify.warning({
          title: me.$t("Notify.Warning"),
          message: h(
            "i",
            { style: "color: teal" },
            me.$t("Notify.ProductNotExists")
          ),
        });
        }
      }
    },
  },
};
</script>

<style lang="scss" scoped>
table {
  tr,
  td {
    border: 1px solid #ddd;
    padding: 8px;
  }
  tr:nth-child(even) {
    background-color: #f2f2f2;
  }
  tr:hover {
    background-color: #ddd;
  }
}
</style>
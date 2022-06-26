<template>
  <div class="overview-container tw-p-6 tw-bg-gray-200 text-yellow-900">
    <div class="number-total tw-grid tw-grid-cols-5 tw-gap-6">
      <div
        class="
          order
          tw-rounded-xl
          tw-bg-red-400
          tw-h-32
          tw-w-full
          tw-flex
          tw-items-center
          tw-p-4
          tw-text-white
          tw-cursor-pointer
        "
      >
        <div class="ic-area tw-h-2/3 tw-w-16">
          <img class="tw-w-full tw-h-full" :src="ic_Order" />
        </div>
        <div class="value tw-ml-5">
          <div class="title tw-text-base">Đơn hàng</div>
          <div class="quantity tw-text-3xl tw-font-semibold">
            {{ orderTotal }}
          </div>
        </div>
      </div>
      <div
        class="
          order
          tw-rounded-xl
          tw-bg-yellow-400
          tw-h-32
          tw-w-full
          tw-flex
          tw-items-center
          tw-p-4
          tw-text-white
          tw-cursor-pointer
        "
      >
        <div class="ic-area tw-h-2/3 tw-w-16">
          <img class="tw-w-full tw-h-full" :src="ic_Cubes" />
        </div>
        <div class="value tw-ml-5">
          <div class="title tw-text-base">Sản phẩm mới</div>
          <div class="quantity tw-text-3xl tw-font-semibold">
            + {{ newProductTotal }}
          </div>
        </div>
      </div>
      <div
        class="
          order
          tw-rounded-xl
          tw-bg-green-400
          tw-h-32
          tw-w-full
          tw-flex
          tw-items-center
          tw-p-4
          tw-text-white
          tw-cursor-pointer
        "
      >
        <div class="ic-area tw-h-2/3 tw-w-16">
          <img class="tw-w-full tw-h-full" :src="ic_Exchange" />
        </div>
        <div class="value tw-ml-5">
          <div class="title tw-text-base">Doanh số</div>
          <div class="quantity tw-text-3xl tw-font-semibold">
            {{ $commonFn.formatMoney2(turnoverTotal.toString()) }}
          </div>
        </div>
      </div>
      <div
        class="
          order
          tw-rounded-xl
          tw-bg-blue-400
          tw-h-32
          tw-w-full
          tw-flex
          tw-items-center
          tw-p-4
          tw-text-white
          tw-cursor-pointer
        "
      >
        <div class="ic-area tw-h-2/3 tw-w-16">
          <img class="tw-w-full tw-h-full" :src="ic_Receipt" />
        </div>
        <div class="value tw-ml-5">
          <div class="title tw-text-base">Phiếu nhập</div>
          <div class="quantity tw-text-3xl tw-font-semibold">
            {{ receiptTotal }}
          </div>
        </div>
      </div>
      <div
        class="
          order
          tw-rounded-xl
          tw-bg-pink-400
          tw-h-32
          tw-w-full
          tw-flex
          tw-items-center
          tw-p-4
          tw-text-white
          tw-cursor-pointer
        "
      >
        <div class="ic-area tw-h-2/3 tw-w-16">
          <img class="tw-w-full tw-h-full" :src="ic_OrderCancelled" />
        </div>
        <div class="value tw-ml-5">
          <div class="title tw-text-base">Đơn hàng bị hủy</div>
          <div class="quantity tw-text-3xl tw-font-semibold">{{quantityOrderCancelled}}</div>
        </div>
      </div>
    </div>
    <div class="chart tw-mt-6 tw-grid tw-grid-cols-2 tw-gap-6">
      <BarChart
      class="tw-bg-white tw-p-6 tw-rounded-lg"
        chartTitle="Doanh số hàng tháng"
        label="Doanh số"
        :chartData="monthlyTurnover"
        :labels="barLabels"
      />
      <DoughnutChart
      class="tw-bg-white tw-p-6 tw-rounded-lg"
        chartTitle="Số lượng sản phẩm bán được của các hãng"
        :chartData="chartDoughnutChartQuantity"
        :labels="chartDoughnutChartName"
      />
    </div>
    
    <div class="table-list tw-mt-6 tw-rounded-lg tw-bg-white tw-p-6">
      <!-- tw-grid tw-grid-cols-2 tw-mt-6 -->
      <div class="best-sellings ">
        <div class="table-title tw-w-full tw-text-center tw-text-xl">Danh sách sản phẩm bán chạy nhất tháng</div>
        <el-table
              class="table"
              :data="productBestSellings"
              height="500px"
              style="width: 100%"
              :lazy="true"
            >
              <el-table-column type="index" width="50"> </el-table-column>
              
              <el-table-column label="Tên sản phẩm" min-width="150">
                <template
                  slot-scope="scope"
                >
                  {{scope.row.Product.ProductName+ " " + scope.row.ProductAttribute.Memory+"GB"}}
                </template>
              </el-table-column>
              <el-table-column label="Ảnh" width="100"  align="center">
                <template
                  slot-scope="scope"
                >
                  <img :src="scope.row.Product.Avatar"/>
                </template>
              </el-table-column>
              <el-table-column prop="SellingQuantity" label="Số lượng" width="100"  align="center">
              </el-table-column>
              
            </el-table>
      </div>
      <!-- <PieChart /> -->
    </div>
    <div class="cancelled-orders tw-mt-6 tw-rounded-lg tw-bg-white tw-p-6">
        <div class="table-title tw-w-full tw-text-center tw-text-xl">Danh sách đơn hàng bị hủy</div>
        <el-table
              class="table"
              :data="cancelledOrders"
              height="500px"
              style="width: 100%"
              :lazy="true"
            >
              <el-table-column type="index" width="50"> </el-table-column>
              
              <el-table-column prop="Order.OrderID" label="Mã đơn hàng" width="150">
                
              </el-table-column>
              <el-table-column label="Số lượng mặt hàng" width="100" >
                
              </el-table-column>
              <el-table-column prop="Order.TotalPrice" label="Tổng giá" width="100"  align="right">
              </el-table-column>
              <el-table-column prop="Order.Note" label="Ghi chú" max-width="300"  >
              </el-table-column>
              <el-table-column prop="Order.DeliveryAddress" label="Địa chỉ giao hàng" min-width="150">
              </el-table-column>
              <el-table-column prop="Customer.CustomerName" label="Tên khách hàng" min-width="150">
              </el-table-column>
              <el-table-column prop="Customer.PhoneNumber" label="Số điện thoại" width="100">
              </el-table-column>
              
            </el-table>
      </div>
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";
import BarChart from "@/components/controls/chart/Bar.vue";
import DoughnutChart from "@/components/controls/chart/Doughnut.vue";
//import PieChart from "@/components/controls/chart/Pie.vue";
export default {
  components: {
    BarChart,
    DoughnutChart,
    //PieChart,
  },
  data() {
    return {
      ic_Order: require("@/assets/icons/overview/order.png"),
      ic_OrderCancelled: require("@/assets/icons/overview/order_cancel.png"),
      ic_Receipt: require("@/assets/icons/overview/receipt.png"),
      ic_Exchange: require("@/assets/icons/overview/exchange.png"),
      ic_Cubes: require("@/assets/icons/overview/cubes.png"),
      barLabels: [
        "Tháng 1",
        "Tháng 2",
        "Tháng 3",
        "Tháng 4",
        "Tháng 5",
        "Tháng 6",
        "Tháng 7",
        "Tháng 8",
        "Tháng 9",
        "Tháng 10",
        "Tháng 11",
        "Tháng 12",
      ],
    };
  },
  computed: {
    ...mapState("overview", [
      "turnoverTotal",
      "orderTotal",
      "receiptTotal",
      "newProductTotal",
      "monthlyTurnover",
      "quantitySoldByManufacturer",
      "quantityOrderCancelled",
      "productBestSellings",
      "cancelledOrders",
      "month"
    ]),
    chartDoughnutChartQuantity() {
      const me = this;
      const quantity = me.quantitySoldByManufacturer.map((item) => {
        return item.Quantity;
      });
      return quantity;
    },
    chartDoughnutChartName() {
      const me = this;
      const name = me.quantitySoldByManufacturer.map((item) => {
        return item.Manufacturer.ManufacturerName;
      });
      return name;
    },
  },
  async created() {
    const me = this;
    me.initData(me.month)
    me.getMonthlyTurnover();
    
    
  },
  methods: {
    ...mapActions("overview", [
      "getTurnoverTotal",
      "getOrderTotal",
      "getReceiptTotal",
      "getNewProductTotal",
      "getMonthlyTurnover",
      "getQuantitySoldByManufacturer",
      "getQuantityOrderCancelled",
      "getProductBestSellingsInMonth",
      "getCancelledOrders"
    ]),
    initData(month){
      const me=this
      return new Promise((resolve,reject)=>{
        me.getTurnoverTotal(month);
        me.getOrderTotal(month);
        me.getReceiptTotal(month);
        me.getNewProductTotal(month);
        me.getProductBestSellingsInMonth({month:month,productNumber:10});
        me.getQuantityOrderCancelled(month);
        me.getQuantitySoldByManufacturer(month);
        me.getCancelledOrders(month);
        resolve(true)

      })
      
    }
  },
  watch:{
    async month(){
      const me=this
      me.$commonFn.showMark("overview-container")
      await me.initData(me.month).then((res)=>{

        me.$commonFn.hideMark("overview-container")
      })
    }
  }
};
</script>

<style lang="scss" scoped>
::v-deep{
  thead{
    background-color: red;
  }
}
</style>
<style scoped>
.overview-container{
  @apply tw-h-auto !important;
}
</style>
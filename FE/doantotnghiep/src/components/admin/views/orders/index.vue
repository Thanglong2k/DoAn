<template>
  <div class="order-list-container tw-w-full">
    <div class="body tw-h-full">
      <div class="filter-area tw-mx-4 tw-h-12 tw-flex tw-justify-between">
        <div class="search tw-w-2/5">
        <span class="tw-mr-4">Tìm kiếm</span>
          <el-input
            v-model="search"
            @input="changeFilter"
            :placeholder="$t('Search')"
          />
        </div>
        <div class="filter tw-flex tw-justify-end">
          <div class="filter-status tw-mr-5">
          <span class="tw-mr-4">Trạng thái</span>
          <el-select
            v-model="status"
            @change="onChangeStatus"
          >
            <el-option
              v-for="item in optionStatus"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            >
                <div class="item-product tw-flex">
                    <div class="product-name">{{item.label}}</div>
                </div>
            </el-option>
          </el-select>
        </div>
        <div class="filter-date">
          <span class="tw-mr-4">Chọn khoảng thời gian</span>
          <el-date-picker
            v-model="dateFilter"
            type="daterange"
            align="right"
            start-placeholder="Ngày bắt đầu"
            end-placeholder="Ngày kết thúc"
            :default-value="now"
            @change="changeFilter"
          >
          </el-date-picker>
        </div>
          <el-button class="export-btn" @click="handleDownload">Xuất Excel</el-button>
        </div>
        
      </div>
      <div class="grid tw-p-5 tw-pb-0 tw-mt-4">
        <el-table
          class="table"
          :data="orders"
          height="calc(100% - 49px - 16px)"
          style="width: 100%"
        >
          <el-table-column type="index" width="50"> </el-table-column>
          <el-table-column
            prop="Order.OrderID"
            label="Mã đơn hàng"
            min-width="150"
          >
          </el-table-column>
          <el-table-column prop="ProductQuantity" label="Số lượng" align="right" width="100">
          </el-table-column>
          <el-table-column prop="Order.TotalPrice" label="Tổng giá" align="right" width="150">
            <template slot-scope="scope">
              {{$commonFn.formatMoney2(scope.row.Order.TotalPrice.toString())}}
            </template>
          </el-table-column>
          <el-table-column
            prop="Customer.CustomerName"
            label="Tên khách hàng"
            min-width="100"
          >
          </el-table-column>
          <el-table-column prop="Order.Note" label="Ghi chú" min-width="150">
          </el-table-column>
          <el-table-column
            prop="Order.OrderStatus"
            label="Trạng thái đơn hàng"
            width="150"
          >
          <template slot-scope="scope">
              {{getOrrderStatusValue(scope.row.Order.OrderStatus)}}
            </template>
          </el-table-column>
          <el-table-column
            prop="Order.PaymentStatus"
            align="center"
            label="Đã thanh toán"
            width="150"
          >
            <template slot-scope="scope">
              <el-button type="success" icon="el-icon-check" v-if="scope.row.Order.PaymentStatus" @click="handleChangePaymentStatusValue(scope.row.Order)"></el-button>
              <el-button type="danger" icon="el-icon-close" v-else @click="handleChangePaymentStatusValue(scope.row.Order)"></el-button>
              <!-- <el-checkbox v-model="scope.row.Order.PaymentStatus" @change="handleChangePaymentStatusValue(scope.row.Order)" /> -->
            </template>
          </el-table-column>

          <el-table-column label="Chức năng" width="200">
            <template slot-scope="scope">
              <el-button
                @click="handleDetail(scope.row.Order.OrderID)"
                type="text"
                size="small"
                >{{ $t("Button.Detail") }}</el-button
              >
              <!-- <el-button
                @click="handleEdit(scope.row)"
                type="text"
                size="small"
                >{{ $t("Button.Edit") }}</el-button
              >
              <el-button
                @click="handleDelete(scope.row.OrderID)"
                type="text"
                size="small"
                >{{ $t("Button.Delete") }}</el-button
              > -->
            </template>
          </el-table-column>
        </el-table>
        <el-pagination
          class="tw-flex tw-justify-end tw-h-12 tw-items-center"
          @size-change="handleSizeChange"
          @current-change="handleCurrentChange"
          :current-page.sync="currentPage"
          :page-size="pageSize"
          :page-sizes="pageSizes"
          layout="total,sizes, prev, pager, next"
          :total="orderTotal"
        >
        </el-pagination>
      </div>
    </div>
    <!-- <CreateOrder
      v-if="show"
      @closePopup="closePopup"
      :typePopup="typePopup"
      :order="order"
      ref="popup"
    /> -->
  </div>
</template>

<script>
import { mapActions, mapState } from "vuex";
export default {
  data() {
    return {
      search: "",
      currentPage: 1,
      pageSizes: [10, 20, 30, 40],
      pageSize: 10,
      order: {},
      typePopup: true,
      show: false,
      dateFilter: "",
      optionStatus:[
        {value:-1,label:'Tất cả'},
        {value:0,label:'Chờ xác nhận'},
        {value:1,label:'Chờ lấy hàng'},
        {value:2,label:'Đang giao'},
        {value:3,label:'Đã giao'},
        {value:4,label:'Đã hủy'},
      ],
      status:-1,
      downloadLoading: false,
      filename: 'Danh sách đơn hàng',
      autoWidth: true,
      bookType: 'xlsx'
    };
  },
  computed: {
    ...mapState("order", ["orders", "orderTotal", "pageTotal"]),
    now() {
      var today = new Date();
      var dd = String(today.getDate()).padStart(2, "0");
      var mm = String(today.getMonth() + 1).padStart(2, "0"); //January is 0!
      var yyyy = today.getFullYear();

      today = yyyy + "/" + mm + "/" + dd;
      return today
    },
  },
  mounted() {
    const me = this;
    me.getOrders(me.currentPage, me.pageSize, me.search);
  },
  methods: {
    ...mapActions("order", ["getOrdersPaging", "deleteOrder","updateOrder"]),
    getOrders(pageIndex, pageSize, queryText) {
      const me = this;
      const params = {
        pageIndex: pageIndex,
        pageSize: pageSize,
        queryText: queryText,
        startTime: me.dateFilter?me.$commonFn.addHours(7,me.dateFilter[0]):'',
        endTime: me.dateFilter?me.$commonFn.addHours(7,me.dateFilter[1]):'',
        status:me.status
      };
      me.$commonFn.showMark("order-list-container")
      
      me.getOrdersPaging(params);
      me.$commonFn.hideMark("order-list-container")
    },
    /**
     * Sửa
     */
    handleEdit(order) {
      const me = this;
      me.order = order;
      me.typePopup = false;
      me.show = true;
      me.$refs.popup.setModel(true);
    },
    /**
     * Xóa
     */
    handleDelete(id) {},
    /**
     * Chi tiết
     */
    handleDetail(id) {
      const me=this
      me.$router.push(`/admin/order/detail?id=${id}`)
    },
    handleSizeChange(val) {
      const me = this;
      me.pageSize = val;
      me.getOrders(me.currentPage, me.pageSize, me.search);
    },
    handleCurrentChange(val) {
      const me = this;
      me.currentPage = val;
      me.getOrders(me.currentPage, me.pageSize, me.search);
    },
    changeFilter() {
      const me = this;
      setTimeout(() => {
        me.currentPage = 1;
        me.pageSize = me.pageSizes[0];
        me.getOrders(me.currentPage, me.pageSize, me.search);
      }, 500);
    },
    handleChangePaymentStatusValue(order){
      const me=this
      order.PaymentStatus=!order.PaymentStatus
      const params={
        id:order.OrderID,
        params:order
      }
      me.$commonFn.showMark("order-list-container")
      
      
      me.updateOrder(params)
      me.$commonFn.hideMark("order-list-container")
    },
    getOrrderStatusValue(orderStatus){
      const me=this
      
      console.log(orderStatus);
      switch(orderStatus){
        case me.$enumeration.OrderStatus.WaitConfirm: return 'Chờ xác nhận'
        case me.$enumeration.OrderStatus.WaitGoods: return 'Chờ lấy hàng'
        case me.$enumeration.OrderStatus.Delivering: return 'Đang giao hàng'
        case me.$enumeration.OrderStatus.Delivered: return 'Đã giao hàng'
        case me.$enumeration.OrderStatus.Cancelled: return 'Đã hủy'
        
      }
    },
    onChangeStatus(){
      const me=this
      me.getOrders(me.currentPage, me.pageSize, me.search)
    },
    handleDownload() {
      this.downloadLoading = true
      const orders=this.orders.map(item=>{
        const order={}
        order.OrderID=item.Order.OrderID
        order.ProductQuantity=item.ProductQuantity
        order.TotalPrice=item.Order.TotalPrice
        order.CustomerName=item.Customer.CustomerName
        order.Note=item.Order.Note
        order.OrderStatus=this.getOrrderStatusValue(item.Order.OrderStatus)
        order.PaymentStatus=item.Order.PaymentStatus?'Đã thanh toán':'Chưa thanh toán'
        return order
      })
      import('@/vendor/Export2Excel').then(excel => {
        const tHeader = ['Mã đơn hàng', 'Số lượng', 'Tổng giá', 'Tên khách hàng', 'Ghi chú','Trạng thái đơn','Đã thanh toán']
        const filterVal = ['OrderID', 'ProductQuantity', 'TotalPrice', 'CustomerName', 'Note', 'OrderStatus','PaymentStatus']
        const list = orders
        const data = this.formatJson(filterVal, list)
        excel.export_json_to_excel({
          header: tHeader,
          data,
          filename: this.filename,
          autoWidth: this.autoWidth,
          sheetname:this.filename,
          bookType: this.bookType
        })
        this.downloadLoading = false
      })
    },
    formatJson(filterVal, jsonData) {
      return jsonData.map(v => filterVal.map(j => {
        
          return v[j]
        
      }))
    }
  },
};
</script>

<style lang="scss" scoped>
.order-list-container {
  //height: calc(100vh - 65px);
  .body {
    .grid {
      height: calc(100% - 49px - 17px);
    }
    .filter-area{
      .filter{
        .filter-status{
          width:150px;
        }
        .filter-date{
          width:360px;
        }
      }
    }
  }
}
</style>
<style scoped>
.export-btn{

  @apply tw-bg-red-500 tw-text-white !important
}
</style>
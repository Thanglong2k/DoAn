<template>
  <div class="receipt-list-container tw-w-full">

    <div class="body tw-h-full">
      <div class="filter-area tw-mx-4 tw-h-12 tw-flex tw-justify-between">
        <div class="search tw-w-2/4">
          <el-input
            v-model="search"
            @input="changeFilter"
            :placeholder="$t('Search')"
          />
        </div>
        <div class="filter">
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
          <el-button class="add-new-btn tw-ml-4" @click="handleAdd">{{$t('Button.AddNew')}}</el-button>
        </div>
      </div>
      <div class="grid tw-p-5 tw-pb-0 tw-mt-4">
        <el-table
          class="table"
          :data="receipts"
          height="calc(100% - 49px - 16px)"
          style="width: 100%"
        >
          <el-table-column type="index" width="50"> </el-table-column>
          <el-table-column
            prop="Receipt.ReceiptID"
            label="Mã đơn hàng"
            min-width="150"
          >
          </el-table-column>
          <el-table-column prop="ReceiptDetailQuantity" label="Số lượng" align="right" width="100">
          </el-table-column>
          <el-table-column prop="Receipt.TotalMoney" label="Tổng giá" align="right" width="150">
            <template slot-scope="scope">
              {{$commonFn.formatMoney2(scope.row.Receipt.TotalMoney.toString())}}
            </template>
          </el-table-column>
          <el-table-column
            prop="Suplier.SuplierName"
            label="Tên khách hàng"
            min-width="150"
          >
          </el-table-column>
          <el-table-column prop="Receipt.Note" label="Ghi chú" min-width="150">
          </el-table-column>
          
          <el-table-column
            prop="Receipt.PaymentStatus"
            align="center"
            label="Đã thanh toán"
            width="150"
          >
            <template slot-scope="scope">
              <el-checkbox v-model="scope.row.Receipt.PaymentStatus" @change="handleChangePaymentStatusValue(scope.row.Receipt)" />
            </template>
          </el-table-column>

          <el-table-column label="Chức năng" width="200">
            <template slot-scope="scope">
              <el-button
                @click="handleDetail(scope.row.Receipt.ReceiptID)"
                type="text"
                size="small"
                >{{ $t("Button.Detail") }}</el-button
              >
              <el-button
                @click="handleEdit(scope.row.Receipt.ReceiptID)"
                type="text"
                size="small"
                >{{ $t("Button.Edit") }}</el-button
              >
              <!-- <el-button
                @click="handleDelete(scope.row.ReceiptID)"
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
          :total="receiptTotal"
        >
        </el-pagination>
      </div>
    </div>
    <!-- <CreateReceipt
      v-if="show"
      @closePopup="closePopup"
      :typePopup="typePopup"
      :receipt="receipt"
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
      receipt: {},
      typePopup: true,
      show: false,
      dateFilter: "",
    };
  },
  computed: {
    ...mapState("receipt", ["receipts", "receiptTotal", "pageTotal"]),
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
    me.getReceipts(me.currentPage, me.pageSize, me.search);
  },
  methods: {
    ...mapActions("receipt", ["getReceiptsPaging", "deleteReceipt","updateReceipt"]),
    getReceipts(pageIndex, pageSize, queryText) {
      const me = this;
      const params = {
        pageIndex: pageIndex,
        pageSize: pageSize,
        queryText: queryText,
        startTime: me.dateFilter?me.$commonFn.addHours(7,me.dateFilter[0]):null,
        endTime: me.dateFilter?me.$commonFn.addHours(7,me.dateFilter[1]):null,
      };
      me.$commonFn.showMark("receipt-list-container")
      me.getReceiptsPaging(params);
      me.$commonFn.hideMark("receipt-list-container")
    },
    /**
     * Sửa
     */
    handleEdit(receiptID) {
      const me=this
      
      me.$router.push(`receipt/create-receipt?receiptID=${receiptID}`)
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
      me.$router.push(`/admin/receipt/detail?id=${id}`)
    },
    handleSizeChange(val) {
      const me = this;
      me.pageSize = val;
      me.getReceipts(me.currentPage, me.pageSize, me.search);
    },
    handleCurrentChange(val) {
      const me = this;
      me.currentPage = val;
      me.getReceipts(me.currentPage, me.pageSize, me.search);
    },
    changeFilter() {
      const me = this;
      setTimeout(() => {
        me.currentPage = 1;
        me.pageSize = me.pageSizes[0];
        me.getReceipts(me.currentPage, me.pageSize, me.search);
      }, 500);
    },
    handleChangePaymentStatusValue(receipt){
      const me=this
      const params={
        id:receipt.ReceiptID,
        params:receipt
      }
      me.$commonFn.showMark("receipt-list-container")
      me.updateReceipt(params)
      me.$commonFn.hideMark("receipt-list-container")
    },
    getOrrderStatusValue(receiptStatus){
      const me=this
      
      console.log(receiptStatus);
      switch(receiptStatus){
        case me.$enumeration.ReceiptStatus.WaitConfirm: return 'Chờ xác nhận'
        case me.$enumeration.ReceiptStatus.WaitGoods: return 'Chờ lấy hàng'
        case me.$enumeration.ReceiptStatus.Delivering: return 'Đang giao hàng'
        case me.$enumeration.ReceiptStatus.Delivered: return 'Đã giao hàng'
        case me.$enumeration.ReceiptStatus.Cancelled: return 'Đã hủy'
        
      }
    },
    /**
     * Thêm mới
     */
   
    handleAdd(){
      const me=this
      me.$router.push("receipt/create-receipt?create=true")
    }
  },
};
</script>

<style lang="scss" scoped>
.receipt-list-container {
  //height: calc(100vh - 65px);
  .body {
    .grid {
      height: calc(100% - 49px - 17px);
    }
  }
}
</style>
<style scoped>
.add-new-btn {
  @apply tw-bg-red-500 tw-text-white !important;
}
</style>
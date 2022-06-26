<template>
  <div class="evaluation-list-container tw-w-full">
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
          <el-date-picker
            v-model="dateFilter"
            type="daterange"
            align="right"
            start-placeholder="Start Date"
            end-placeholder="End Date"
            :default-value="now"
            @change="changeFilter"
          >
          </el-date-picker>
          <el-button class="add-new-btn" @click="handleAdd">{{$t('Button.AddNew')}}</el-button>
        </div>
      </div>
      <div class="grid tw-p-5 tw-pb-0 tw-mt-4">
        <el-table
          class="table"
          :data="evaluations"
          height="calc(100% - 49px - 16px)"
          style="width: 100%"
        >
          <el-table-column type="index" width="50"> </el-table-column>
          <el-table-column
            prop="Product.ProductName"
            label="Tên sản phẩm"
            min-width="150"
          >
          </el-table-column>
          <el-table-column prop="Product.Avatar" label="Ảnh" align="center" width="100">
            <template slot-scope="scope">
              <img :src="scope.row.Product.Avatar"/>
            </template> 
          </el-table-column>
          
          <el-table-column
            prop="Evaluation.Content"
            label="Nội dung"
            min-width="100"
          >
          </el-table-column>
          <el-table-column
            prop="Evaluation.Rate"
            label="Đánh giá"
            min-width="100"
          >
          </el-table-column>
          <el-table-column prop="Customer.CustomerName" label="Tác giả" min-width="100">
            <template slot-scope="scope">
              {{scope.row.Customer.CustomerName?scope.row.Customer.CustomerName:scope.row.Account.FullName+ ' (Admin)'}}
            </template> 
          </el-table-column>
          <el-table-column prop="Customer.CustomerName" label="Hiển thị" align="center" width="100">
            <template slot-scope="scope">
              <el-Checkbox v-model="scope.row.Evaluation.Status" @change="changeStatus(scope.row.Evaluation)"/>
            </template> 
          </el-table-column>
          
          <!-- <el-table-column label="Chức năng" width="200">
            <template slot-scope="scope">
              <el-button
                @click="handleReply(scope.row)"
                type="text"
                size="small"
                >Trả lời</el-button
              >
              <el-button
                @click="handleDelete(scope.row.Evaluation.EvaluationID)"
                type="text"
                size="small"
                >{{ $t("Button.Delete") }}</el-button
              >
            </template>
          </el-table-column> -->
        </el-table>
        <el-pagination
          class="tw-flex tw-justify-end tw-h-12 tw-items-center"
          @size-change="handleSizeChange"
          @current-change="handleCurrentChange"
          :current-page.sync="currentPage"
          :page-size="pageSize"
          :page-sizes="pageSizes"
          layout="total,sizes, prev, pager, next"
          :total="evaluationTotal"
        >
        </el-pagination>
      </div>
    </div>
    <ReplyEvaluation
      @closePopup="closePopup"
      :typePopup="typePopup"
      :evaluation="evaluation"
      ref="popup"
    />
  </div>
</template>

<script>
import { mapActions, mapState } from "vuex";
import ReplyEvaluation from './popup/reply-evaluation.vue'
export default {
  components:{
    ReplyEvaluation
  },
  data() {
    return {
      search: "",
      currentPage: 1,
      pageSizes: [10, 20, 30, 40],
      pageSize: 10,
      evaluation: {},
      typePopup: true,
      show: false,
      dateFilter: "",
    };
  },
  computed: {
    ...mapState("evaluation", ["evaluations", "evaluationTotal", "pageTotal"]),
    now() {
      var today = new Date();
      var dd = String(today.getDate()).padStart(2, "0");
      var mm = String(today.getMonth() + 1).padStart(2, "0"); //January is 0!
      var yyyy = today.getFullYear();

      today = yyyy + "/" + mm + "/" + dd;
      return today
    },
  },
  created() {
    const me = this;
    me.getEvaluations(me.currentPage, me.pageSize, me.search);
  },
  methods: {
    ...mapActions("evaluation", ["getEvaluationsPaging", "deleteEvaluation","updateEvaluationOfProduct"]),
    getEvaluations(pageIndex, pageSize, queryText) {
      const me = this;
      const params = {
        pageIndex: pageIndex,
        pageSize: pageSize,
        queryText: queryText
      };
      me.getEvaluationsPaging(params);
    },
    closePopup(){},
    
    /**
     * Xóa
     */
    handleDelete(id) {
      const me=this
const h = this.$createElement;
      me.deleteEvaluation(id).then(res=>{
        me.$notify({
                title: me.$t("Notify.Success"),
                message: h(
                  "i",
                  { style: "color: teal" },
                  me.$t("Notify.DeleteSuccess",{object:"bình luận"})
                ),
                type: "success",
              });
              
      }).catch(e=>{
        me.$notify.error({
                title: me.$t("Notify.Error"),
                message: h(
                  "i",
                  { style: "color: teal" },
                  me.$t("Notify.HasError")
                ),
              });
      })
      me.getEvaluations(me.currentPage,me.pageSize,me.queryText)
    },
    handleReply(evaluation){
      const me=this
      me.evaluation=evaluation
      me.show=true
      me.$refs.popup.setModel(true)
    },
    
    handleSizeChange(val) {
      const me = this;
      me.pageSize = val;
      me.getEvaluations(me.currentPage, me.pageSize, me.search);
    },
    handleCurrentChange(val) {
      const me = this;
      me.currentPage = val;
      me.getEvaluations(me.currentPage, me.pageSize, me.search);
    },
    changeFilter() {
      const me = this;
      setTimeout(() => {
        me.currentPage = 1;
        me.pageSize = me.pageSizes[0];
        me.getEvaluations(me.currentPage, me.pageSize, me.search);
      }, 500);
    },
    handleChangePaymentStatusValue(evaluation){
      const me=this
      const params={
        id:evaluation.EvaluationID,
        params:evaluation
      }
      me.updateEvaluation(params)
    },
    getOrrderStatusValue(evaluationStatus){
      const me=this
      
      console.log(evaluationStatus);
      switch(evaluationStatus){
        case me.$enumeration.EvaluationStatus.WaitConfirm: return 'Chờ xác nhận'
        case me.$enumeration.EvaluationStatus.WaitGoods: return 'Chờ lấy hàng'
        case me.$enumeration.EvaluationStatus.Delivering: return 'Đang giao hàng'
        case me.$enumeration.EvaluationStatus.Delivered: return 'Đã giao hàng'
        case me.$enumeration.EvaluationStatus.Cancelled: return 'Đã hủy'
        
      }
    },
    /**
     * Thêm mới
     */
   
    handleAdd(){
      const me=this
      me.$router.push("evaluation/create-evaluation?create=true")
    },
    async changeStatus(evaluation){
      const me=this
      evaluation.CreatedBy=JSON.parse(sessionStorage.getItem("Account")).UserName
      const params={
        id:evaluation.EvaluationID,
        Evaluation:evaluation
      }
    const h = this.$createElement;

      await me.updateEvaluationOfProduct(params).then((res=>{
        me.$notify({
                title: me.$t("Notify.Success"),
                message: h(
                  "i",
                  { style: "color: teal" },
                  me.$t("Notify.UpdateSuccess",{object:"bình luận"})
                ),
                type: "success",
              });
              
      })).catch(e=>{
        me.$notify.error({
                title: me.$t("Notify.Error"),
                message: h(
                  "i",
                  { style: "color: teal" },
                  me.$t("Notify.HasError")
                ),
              });
      })
      me.getEvaluations(me.currentPage,me.pageSize,me.queryText)
    }
  },
};
</script>

<style lang="scss" scoped>
.evaluation-list-container {
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
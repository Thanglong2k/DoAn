<template>
  <div class="comment-list-container tw-w-full">
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
          :data="comments"
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
          <el-table-column prop="ParentComment" label="Bình luân gốc" width="200">
          </el-table-column>
          <el-table-column
            prop="Comment.Content"
            label="Nội dung"
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
              <el-Checkbox v-model="scope.row.Comment.Status" @change="changeStatus(scope.row.Comment)"/>
            </template> 
          </el-table-column>
          
          <el-table-column label="Chức năng" width="200">
            <template slot-scope="scope">
              <el-button
                @click="handleReply(scope.row)"
                type="text"
                size="small"
                >Trả lời</el-button
              >
              <el-button
                @click="handleDelete(scope.row.Comment.CommentID)"
                type="text"
                size="small"
                >{{ $t("Button.Delete") }}</el-button
              >
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
          :total="commentTotal"
        >
        </el-pagination>
      </div>
    </div>
    <ReplyComment
      @closePopup="closePopup"
      :typePopup="typePopup"
      :comment="comment"
      ref="popup"
    />
  </div>
</template>

<script>
import { mapActions, mapMutations, mapState } from "vuex";
import ReplyComment from './popup/reply-comment.vue'
export default {
  components:{
    ReplyComment
  },
  data() {
    return {
      search: "",
      currentPage: 1,
      pageSizes: [10, 20, 30, 40],
      pageSize: 10,
      comment: {},
      typePopup: true,
      show: false,
      dateFilter: "",
    };
  },
  computed: {
    ...mapState("comment", ["comments", "commentTotal", "pageTotal"]),
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
    me.getComments(me.currentPage, me.pageSize, me.search);

      // Make sure to cleanup SignalR event handlers when removing the component
    me.$productHub.$on("comment-added-to-admin", me.onCommentAddedToProduct);

  },
  methods: {
    ...mapActions("comment", ["getCommentsPaging", "deleteComment","updateCommentOfProduct"]),
    ...mapMutations("comment", ["setComments"]),
    onCommentAddedToProduct(comment){
      const me=this
      console.log(comment);
      me.setComments(comment)
    },
    getComments(pageIndex, pageSize, queryText) {
      const me = this;
      const params = {
        pageIndex: pageIndex,
        pageSize: pageSize,
        queryText: queryText
      };
      me.getCommentsPaging(params);
    },
    closePopup(){},
    
    /**
     * Xóa
     */
    handleDelete(id) {
      const me=this
const h = this.$createElement;
      me.deleteComment(id).then(res=>{
        me.$notify({
                title: me.$t("Notify.Success"),
                message: h(
                  "i",
                  { style: "color: teal" },
                  me.$t("Notify.DeleteSuccess",{object:"bình luận"})
                ),
                type: "success",
              });
        me.getComments(me.currentPage,me.pageSize,me.queryText)
              
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
      
    },
    handleReply(comment){
      const me=this
      me.comment=comment
      me.show=true
      me.$refs.popup.setModel(true)
    },
    
    handleSizeChange(val) {
      const me = this;
      me.pageSize = val;
      me.getComments(me.currentPage, me.pageSize, me.search);
    },
    handleCurrentChange(val) {
      const me = this;
      me.currentPage = val;
      me.getComments(me.currentPage, me.pageSize, me.search);
    },
    changeFilter() {
      const me = this;
      setTimeout(() => {
        me.currentPage = 1;
        me.pageSize = me.pageSizes[0];
        me.getComments(me.currentPage, me.pageSize, me.search);
      }, 500);
    },
    handleChangePaymentStatusValue(comment){
      const me=this
      const params={
        id:comment.CommentID,
        params:comment
      }
      me.updateComment(params)
    },
    getOrrderStatusValue(commentStatus){
      const me=this
      
      console.log(commentStatus);
      switch(commentStatus){
        case me.$enumeration.CommentStatus.WaitConfirm: return 'Chờ xác nhận'
        case me.$enumeration.CommentStatus.WaitGoods: return 'Chờ lấy hàng'
        case me.$enumeration.CommentStatus.Delivering: return 'Đang giao hàng'
        case me.$enumeration.CommentStatus.Delivered: return 'Đã giao hàng'
        case me.$enumeration.CommentStatus.Cancelled: return 'Đã hủy'
        
      }
    },
    /**
     * Thêm mới
     */
   
    handleAdd(){
      const me=this
      me.$router.push("comment/create-comment?create=true")
    },
    async changeStatus(comment){
      const me=this
      comment.CreatedBy=JSON.parse(sessionStorage.getItem("Account")).UserName
      const params={
        id:comment.CommentID,
        Comment:comment
      }
    const h = this.$createElement;

      await me.updateCommentOfProduct(params).then((res=>{
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
      me.getComments(me.currentPage,me.pageSize,me.queryText)
    }
  },
  beforeDestroy() {
    const me=this
      // Make sure to cleanup SignalR event handlers when removing the component
      me.$productHub.$off("comment-added-to-admin", me.onCommentAddedToProduct);
      
    },
};
</script>

<style lang="scss" scoped>
.comment-list-container {
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
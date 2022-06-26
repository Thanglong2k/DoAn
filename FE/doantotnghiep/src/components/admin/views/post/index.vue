<template>
  <div class="post-list-container tw-w-full">
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
          :data="posts"
          height="calc(100% - 49px - 16px)"
          style="width: 100%"
        >
          <el-table-column type="index" width="50"> </el-table-column>
          <el-table-column prop="Post.Avatar" label="Ảnh" align="center" width="100">
            <template slot-scope="scope">
              <img :src="scope.row.Post.Avatar"/>
            </template> 
          </el-table-column>
          <el-table-column prop="Post.Title" label="Tiêu đề" min-width="150" max-width="250">
          </el-table-column>
          <el-table-column
          class="content-cell"
            prop="Post.Content"
            label="Nội dung"
            min-width="150"
            max-width="300"
          >
          </el-table-column>
          <el-table-column prop="Product.ProductName" label="Tên sản phẩm" min-width="125">
          </el-table-column>
          <el-table-column prop="Account.FullName" label="Tác giả" min-width="100">
          </el-table-column>
          <el-table-column prop="Post.Status" label="Trạng thái" width="100">
            <template slot-scope="scope">
              <el-button class="button-status display " v-if="scope.row.Post.Status" @click="changeStatus(scope.row.Post)">Hiển thị</el-button>
              <el-button class="button-status lock" v-else @click="changeStatus(scope.row.Post)">Khóa</el-button>
            </template> 
          </el-table-column>
          
          <el-table-column label="Chức năng" width="200">
            <template slot-scope="scope">
              <el-button
                @click="handleEdit(scope.row.Post)"
                type="text"
                size="small"
                >{{ $t("Button.Edit") }}</el-button
              >
              <el-button
                @click="handleDelete(scope.row.Post.PostID)"
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
          :total="postTotal"
        >
        </el-pagination>
      </div>
    </div>
  </div>
</template>

<script>
import { mapActions, mapState } from "vuex";
export default {
  components:{
  },
  data() {
    return {
      search: "",
      currentPage: 1,
      pageSizes: [10, 20, 30, 40],
      pageSize: 10,
      post: {},
      typePopup: true,
      show: false,
      dateFilter: "",
    };
  },
  computed: {
    ...mapState("post", ["posts", "postTotal", "pageTotal"]),
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
    me.getPosts(me.currentPage, me.pageSize, me.search);
  },
  methods: {
    ...mapActions("post", ["getPostsPaging", "deletePost","updatePostOfProduct"]),
    getPosts(pageIndex, pageSize, queryText) {
      const me = this;
      const params = {
        pageIndex: pageIndex,
        pageSize: pageSize,
        queryText: queryText
      };
      me.getPostsPaging(params);
    },
    closePopup(){},
    
    /**
     * Xóa
     */
    handleDelete(id) {
      const me=this
const h = this.$createElement;
      me.deletePost(id).then(res=>{
        me.$notify({
                title: me.$t("Notify.Success"),
                message: h(
                  "i",
                  { style: "color: teal" },
                  me.$t("Notify.DeleteSuccess",{object:"bài viết"})
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
      me.getPosts(me.currentPage,me.pageSize,me.queryText)
    },
    handleEdit(post){
      const me=this
      me.$router.push(`post/create-post?id=${post.PostID}`)
    },

    handleSizeChange(val) {
      const me = this;
      me.pageSize = val;
      me.getPosts(me.currentPage, me.pageSize, me.search);
    },
    handleCurrentChange(val) {
      const me = this;
      me.currentPage = val;
      me.getPosts(me.currentPage, me.pageSize, me.search);
    },
    changeFilter() {
      const me = this;
      setTimeout(() => {
        me.currentPage = 1;
        me.pageSize = me.pageSizes[0];
        me.getPosts(me.currentPage, me.pageSize, me.search);
      }, 500);
    },
    /**
     * Thêm mới
     */
   
    handleAdd(){
      const me=this
      me.$router.push("post/create-post?create=true")
    },
    async changeStatus(post){
      const me=this
      post.Status=!post.Status
      let formData = new FormData();
          for (var item in post) {
            if (post[item] !== null) {
              if (
                (Array.isArray(post[item]) ||
                  typeof post[item] === "object") &&
                item !== "AvatarFile"
              ) {
                post[item] = JSON.stringify(post[item]);
              }
            
                formData.append(item, post[item]);
              
            }
          }
      const params = {
              id: post.PostID,
              formData: formData,
            };
    const h = this.$createElement;

      await me.updatePostOfProduct(params).then((res=>{
        me.$notify({
                title: me.$t("Notify.Success"),
                message: h(
                  "i",
                  { style: "color: teal" },
                  me.$t("Notify.UpdateSuccess",{object:"bài viết"})
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
      me.getPosts(me.currentPage,me.pageSize,me.queryText)
    }
  },
};
</script>

<style lang="scss" scoped>
.post-list-container {
  //height: calc(100vh - 65px);
  .body {
    .grid {
      height: calc(100% - 49px - 17px);
      .button-status{
        width:70px;
      }
    }
  }
}
::v-deep{
  .el-table__row{

    .cell{

              display: -webkit-box;
      -webkit-line-clamp: 3;
      -webkit-box-orient: vertical;
      text-overflow: ellipsis;

    }
  }
}
</style>
<style scoped>
.add-new-btn {
  @apply tw-bg-red-500 tw-text-white !important;
}
.display{
  @apply tw-bg-green-500 tw-text-white tw-flex tw-justify-center !important;
}
.lock{
  @apply tw-bg-red-500 tw-text-white tw-flex tw-justify-center !important;
}
</style>
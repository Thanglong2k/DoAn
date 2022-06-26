<template>
  <div class="category-list-container tw-w-full">
    <div class="body tw-h-full">
      <div class="filter tw-mx-4 tw-h-12 tw-flex tw-justify-between">
        <div class="search tw-w-3/4">
          <el-input
            v-model="search"
            @input="searchCategory"
            :placeholder="$t('Search')"
          />
        </div>
        <el-button class="add-new-btn" @click="handleAdd">{{$t('Button.AddNew')}}</el-button>
      </div>
      <div class="grid tw-p-5 tw-pb-0 tw-mt-4">
        <el-table
          class="table"
          :data="categoryAll"
          height="calc(100% - 49px - 16px)"
          style="width: 100%"
        >
          <el-table-column type="index" width="50"> </el-table-column>
          <el-table-column prop="CategoryName" label="Danh mục" min-width="150">
          </el-table-column>
          <el-table-column prop="Meta" label="Meta" min-width="100">
          </el-table-column>
          <el-table-column prop="CategoryParentID" label="Danh mục cha" min-width="100">
          </el-table-column>
          
          <el-table-column label="Chức năng" width="120">
            <template slot-scope="scope">
              <el-button @click="handleEdit(scope.row)" type="text" size="small"
                >{{$t('Button.Edit')}}</el-button
              >
              <el-button @click="handleDelete(scope.row.CategoryID)" type="text" size="small"
                >{{$t('Button.Delete')}}</el-button
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
          :total="categoryTotal"
        >
        </el-pagination>
      </div>
    </div>
    <CreateCategory
      v-if="show"
      @closePopup="closePopup"
      :typePopup="typePopup"
      :category="category"
      ref="popup"
    />
  </div>
</template>

<script>
import { mapActions, mapState } from "vuex";
import CreateCategory from "./popup/create-category.vue";

export default {
  components: {
    CreateCategory,
  },
data() {
    return {
      search: "",
      currentPage: 1,
      pageSizes:[10, 20, 30, 40],
      pageSize:10,
      category: {},
      typePopup: true,
      show: false,
    };
  },
  computed: {
    ...mapState("category", ["categoryAll","categoryTotal","pageTotal"]),
  },
  created() {
    const me = this;
    me.getCategories(me.currentPage,me.pageSize,me.search)
  },
  methods: {
    ...mapActions("category", ["getCategoriesPaging","deleteCategory"]),
    getCategories(pageIndex,pageSize,queryText){
      const me=this
      const params={
        pageIndex:pageIndex,
        pageSize:pageSize,
        queryText:queryText
      }
      me.getCategoriesPaging(params);
    },
    /**
     * Sửa
     */
    handleEdit(category) {
      const me=this
      me.category = category;
      me.typePopup = false;
      me.show=true

      me.$nextTick(()=>{

        me.$refs.popup.setModel(true);
      })
    },
    /**
     * Xóa
     */
    handleDelete(id) {
      const me = this;
      this.$confirm("Bạn có chắc chắn muốn xóa bản ghi này?", "Cảnh báo", {
        confirmButtonText: "Đồng ý",
        cancelButtonText: "Hủy",
        type: "warning",
      }).then(async () => {
        await me
          .deleteCategory(id)
          .then((res) => {
            const h = this.$createElement;
            console.log(res);
            if (res.MISACode === "MISA-004") {
              this.$notify.error({
                title: me.$t("Notify.Error"),
                message: h(
                  "i",
                  { style: "color: teal" },
                  me.$t("Notify.HasError")
                ),
              });
            } else {
              this.$notify({
                title: me.$t("Notify.Success"),
                message: h(
                  "i",
                  { style: "color: teal" },
                  me.$t("Notify.DeleteSuccess",{object:'danh mục'})
                ),
                type: "success",
              });
              me.getCategories(me.currentPage, me.pageSize, me.search);
            }
          })
          .catch(() => {
            const h = this.$createElement;

            this.$notify.error({
              title: me.$t("Notify.Error"),
              message: h(
                "i",
                { style: "color: teal" },
                me.$t("Notify.HasError")
              ),
            });
          });
      });
    },
    handleSizeChange(val) {
      const me=this
      me.pageSize=val
      me.getCategories(me.currentPage,me.pageSize,me.search)
    },
    handleCurrentChange(val) {
      const me=this
      me.currentPage=val
      me.getCategories(me.currentPage,me.pageSize,me.search)
    },
    searchCategory() {
      const me = this;
      setTimeout(() => {
        me.currentPage = 1;
        me.pageSize = me.pageSizes[0];
        me.getCategories(me.currentPage, me.pageSize, me.search);
      }, 500);
    },
    /**
     * Thêm mới
     */
    handleAdd(){
      const me=this
      me.show=true
      me.$refs.popup.setModel(true);
    },
    closePopup() {
      const me = this;
      me.show = false;
    },
  },
}
</script>

<style lang="scss" scoped>
.category-list-container {
  //height: calc(100vh - 65px);
  .body {
    .grid {
      height: calc(100% - 49px - 17px);
    }
  }
}
</style>
<style scoped>
.add-new-btn{
  @apply tw-bg-red-500 tw-text-white !important;
}
</style>
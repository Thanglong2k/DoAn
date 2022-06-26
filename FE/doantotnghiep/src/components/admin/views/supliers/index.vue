<template>
  <div class="suplier-list-container">
    <div class="body tw-h-full">
      <div class="filter tw-mx-4 tw-h-12 tw-flex tw-justify-between">
        <div class="search tw-w-3/4">
          <el-input
            v-model="search"
            @input="searchAccount"
            :placeholder="$t('Search')"
          />
        </div>
        <el-button class="add-new-btn" @click="handleAdd">{{$t('Button.AddNew')}}</el-button>
      </div>
      <div class="grid tw-p-5 tw-pb-0 tw-mt-4">
        <el-table
        v-loading="loading"
          class="table"
          :data="supliers"
          height="calc(100% - 49px - 16px)"
          style="width: 100%"
        >
          <el-table-column type="index" width="50"> </el-table-column>
          <el-table-column prop="SuplierName" label="Tên nhà cung cấp" min-width="150">
          </el-table-column>
          <el-table-column prop="PhoneNumber" label="Số điện thoại" width="100">
          </el-table-column>
          <el-table-column prop="Email" label="Email" min-width="100">
          </el-table-column>
          <el-table-column prop="Address" label="Địa chỉ" min-width="100">
          </el-table-column>
          <el-table-column label="Chức năng" width="120">
            <template slot-scope="scope">
              <el-button @click="handleEdit(scope.row)" type="text" size="small"
                >{{$t('Button.Edit')}}</el-button
              >
              <el-button @click="handleDelete(scope.row.SuplierID)" type="text" size="small"
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
          :total="suplierTotal"
        >
        </el-pagination>
      </div>
    </div>
  <CreateSuplier
      v-if="show"
      @closePopup="closePopup"
      :typePopup="typePopup"
      :suplier="suplier"
      ref="popup"
    />
  </div>
</template>

<script>
import { mapActions, mapState } from "vuex";
import CreateSuplier from "./popup/create-suplier.vue";
export default {
components: {
    CreateSuplier,
  },
data() {
    return {
      search: "",
      currentPage: 1,
      pageSizes:[10, 20, 30, 40],
      pageSize:10,
      suplier: {},
      typePopup: true,
      show: false,
      loading:true
    };
  },
  computed: {
    ...mapState("suplier", ["supliers","suplierTotal","pageTotal"]),
  },
  created() {
    const me = this;
    debugger
    me.getSupliers(me.currentPage,me.pageSize,me.search)
  },
  methods: {
    ...mapActions("suplier", ["getSupliersPaging","deleteSuplier"]),
    getSupliers(pageIndex,pageSize,queryText){
      const me=this
      const params={
        pageIndex:pageIndex,
        pageSize:pageSize,
        queryText:queryText
      }
       new Promise(resolve=>{
        me.getSupliersPaging(params);
        return resolve(true)
      }).then(res=>{
        me.loading=false
      })
    },
    
    /**
     * Sửa
     */
    handleEdit(suplier) {
      const me=this
      me.suplier = suplier;
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
          .deleteSuplier(id)
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
                  me.$t("Notify.DeleteSuccess",{object:"nhà cung cấp"})
                ),
                type: "success",
              });
              me.getSupliers(me.currentPage, me.pageSize, me.search);
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
      me.getSupliers(me.currentPage,me.pageSize,me.search)
    },
    handleCurrentChange(val) {
      const me=this
      me.currentPage=val
      me.getSupliers(me.currentPage,me.pageSize,me.search)
    },
    searchAccount() {
      const me = this;
      setTimeout(() => {
        me.currentPage = 1;
        me.pageSize = me.pageSizes[0];
        me.getSupliers(me.currentPage, me.pageSize, me.search);
      }, 500);
    },
    /**
     * Thêm mới
     */
    handleAdd(){
      const me=this
      me.suplier = {};
      me.typePopup = true;
      me.show=true
      me.$nextTick(()=>{
        me.$refs.popup.setModel(true);
      })
    },
    closePopup() {
      const me = this;
      me.show = false;
    },
  },
}
</script>

<style lang="scss">
.suplier-list-container {
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
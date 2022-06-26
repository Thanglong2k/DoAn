<template>
  <div class="manufacturer-list-container tw-w-full">
    <div class="body tw-h-full">
      <div class="filter tw-mx-4 tw-h-12 tw-flex tw-justify-between">
        <div class="search tw-w-3/4">
          <el-input
            v-model="search"
            @input="searchManufacturer"
            :placeholder="$t('Search')"
          />
        </div>
        <el-button class="add-new-btn" @click="handleAdd">{{
          $t("Button.AddNew")
        }}</el-button>
      </div>
      <div class="grid tw-p-5 tw-pb-0 tw-mt-4">
        <el-table
          class="table"
          :data="manufacturers"
          height="calc(100% - 49px - 16px)"
          style="width: 100%"
        >
          <el-table-column type="index" width="50"> </el-table-column>
          <el-table-column
            prop="ManufacturerName"
            label="Hãng sản xuất"
            min-width="150"
          >
          </el-table-column>

          <el-table-column label="Chức năng" prop="ManufacturerID" width="120">
            <template slot-scope="scope">
              <el-button
                @click="handleEdit(scope.row)"
                type="text"
                size="small"
                >{{ $t("Button.Edit") }}</el-button
              >
              <el-button
                @click="handleDelete(scope.row.ManufacturerID)"
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
          :total="manufacturerTotal"
        >
        </el-pagination>
      </div>
    </div>
    <CreateManufacturer
      v-if="show"
      @closePopup="closePopup"
      :typePopup="typePopup"
      :manufacturer="manufacturer"
      ref="popup"
    />
  </div>
</template>

<script>
import { mapActions, mapState } from "vuex";
import CreateManufacturer from "./popup/create-manufacturer.vue";
export default {
  components: {
    CreateManufacturer,
  },
  data() {
    return {
      search: "",
      currentPage: 1,
      pageSizes: [10, 20, 30, 40],
      pageSize: 10,
      manufacturer: {},
      typePopup: true,
      show: false,
    };
  },
  computed: {
    ...mapState("manufacturer", [
      "manufacturers",
      "manufacturerTotal",
      "pageTotal",
    ]),
  },
  created() {
    const me = this;
    me.getManufacturers(me.currentPage, me.pageSize, me.search);
  },
  methods: {
    ...mapActions("manufacturer", [
      "getManufacturersPaging",
      "deleteManufacturer",
    ]),
    getManufacturers(pageIndex, pageSize, queryText) {
      const me = this;
      const params = {
        pageIndex: pageIndex,
        pageSize: pageSize,
        queryText: queryText,
      };
      me.getManufacturersPaging(params);
    },
    /**
     * Sửa
     */
    handleEdit(manufacturer) {
      const me = this;

      me.manufacturer = manufacturer;
      me.typePopup = false;
      me.show = true;
      me.$nextTick(()=>{

        me.$refs.popup.setModel(true);
      })
    },
    closePopup() {
      const me = this;
      me.show = false;
    },
    /**
     * Xóa
     */
    async handleDelete(id) {
      const me = this;
      this.$confirm("Bạn có chắc chắn muốn xóa bản ghi này?", "Cảnh báo", {
        confirmButtonText: "Đồng ý",
        cancelButtonText: "Hủy",
        type: "warning",
      }).then(async () => {
        const h = this.$createElement;
        await me
          .deleteManufacturer(id)
          .then((res) => {
            
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
                  me.$t("Notify.DeleteSuccess",{object:'hãng sản xuất'})
                ),
                type: "success",
              });
              me.getManufacturers(me.currentPage, me.pageSize, me.search);
            }
          })
          .catch(() => {
            

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
      const me = this;
      me.pageSize = val;
      me.getManufacturers(me.currentPage, me.pageSize, me.search);
    },
    handleCurrentChange(val) {
      const me = this;
      me.currentPage = val;
      me.getManufacturers(me.currentPage, me.pageSize, me.search);
    },
    searchManufacturer() {
      const me = this;
      setTimeout(() => {
        me.currentPage = 1;
        me.pageSize = me.pageSizes[0];
        me.getManufacturers(me.currentPage, me.pageSize, me.search);
      }, 500);
    },
    /**
     * Thêm mới
     */
    handleAdd() {
      const me = this;
      me.show = true;
      me.$refs.popup.setModel(true);
    },
  },
};
</script>

<style lang="scss" scoped>
.manufacturer-list-container {
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
<template>
  <div class="customer-list-container">
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
          :data="customers"
          height="calc(100% - 49px - 16px)"
          style="width: 100%"
        >
          <el-table-column type="index" width="50"> </el-table-column>
          <el-table-column prop="CustomerName" label="Tên khách hàng" min-width="150">
          </el-table-column>
          <el-table-column label="Giới tính" width="100">
            <template slot-scope="scope">
              {{getGender(scope.row.Gender)}}
            </template>
          </el-table-column>
          <el-table-column prop="PhoneNumber" label="Số điện thoại" width="100">
          </el-table-column>
          <el-table-column prop="Email" label="Email" min-width="100">
          </el-table-column>
          <!-- <el-table-column label="Chức năng" width="120">
            <template slot-scope="scope">
              <el-button @click="handleEdit(scope.row)" type="text" size="small"
                >{{$t('Button.Edit')}}</el-button
              >
              <el-button @click="handleDelete(scope.row.CustomerID)" type="text" size="small"
                >{{$t('Button.Delete')}}</el-button
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
          :total="customerTotal"
        >
        </el-pagination>
      </div>
    </div>

  </div>
</template>

<script>
import { mapActions, mapState } from "vuex";

export default {

data() {
    return {
      search: "",
      currentPage: 1,
      pageSizes:[10, 20, 30, 40],
      pageSize:10,
      customer: {},
      typePopup: true,
      show: false,
      loading:true
    };
  },
  computed: {
    ...mapState("customer", ["customers","customerTotal","pageTotal"]),
  },
  created() {
    const me = this;
    me.getCustomers(me.currentPage,me.pageSize,me.search)
  },
  methods: {
    ...mapActions("customer", ["getCustomersPaging","deleteCustomer"]),
    getCustomers(pageIndex,pageSize,queryText){
      const me=this
      const params={
        pageIndex:pageIndex,
        pageSize:pageSize,
        queryText:queryText
      }
       new Promise(resolve=>{
        me.getCustomersPaging(params);
        return resolve(true)
      }).then(res=>{
        me.loading=false
      })
    },
    getGender(gender){
      const me=this
      switch(gender){
        case me.$enumeration.Gender.Female:
          return "Nữ"
        default:
          return "Nam"
      }
    },
    /**
     * Sửa
     */
    handleEdit(customer) {
      const me=this
      me.customer = customer;
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
          .deleteCustomer(id)
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
                  me.$t("Notify.DeleteSuccess",{object:"tài khoản"})
                ),
                type: "success",
              });
              me.getCustomers(me.currentPage, me.pageSize, me.search);
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
      me.getCustomers(me.currentPage,me.pageSize,me.search)
    },
    handleCurrentChange(val) {
      const me=this
      me.currentPage=val
      me.getCustomers(me.currentPage,me.pageSize,me.search)
    },
    searchAccount() {
      const me = this;
      setTimeout(() => {
        me.currentPage = 1;
        me.pageSize = me.pageSizes[0];
        me.getCustomers(me.currentPage, me.pageSize, me.search);
      }, 500);
    },
    /**
     * Thêm mới
     */
    handleAdd(){
      const me=this
      me.customer = {};
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
.customer-list-container {
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
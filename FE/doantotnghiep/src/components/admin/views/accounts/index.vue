<template>
  <div class="account-list-container">
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
          :data="accounts"
          height="calc(100% - 49px - 16px)"
          style="width: 100%"
        >
          <el-table-column type="index" width="50"> </el-table-column>
          <el-table-column prop="UserName" label="Tài khoản" min-width="150">
          </el-table-column>
          <el-table-column prop="Password" label="Mật khẩu" min-width="100">
          </el-table-column>
          <el-table-column prop="FullName" label="Họ tên" min-width="100">
          </el-table-column>
          <el-table-column prop="Email" label="Email" min-width="100">
          </el-table-column>
          <el-table-column prop="PhoneNumber" label="Số điện thoại" width="100">
          </el-table-column>
          <el-table-column prop="Permission" label="Quyền" width="100">
            <template slot-scope="scope">
              {{scope.row.Permission===$enumeration.AccountPermission.Admin?"Quản trị":"Nhân viên"}}
            </template>
          </el-table-column>
          <el-table-column prop="Status" label="Hoạt động" width="100">
            <template slot-scope="scope">
              <el-button class="status-btn" :class="[{'activated':scope.row.Status},{'locked':!scope.row.Status}]" @click="handleChangeStatusValue(scope.row)">{{scope.row.Status?"Kích hoạt":"Khóa"}}</el-button>
            </template>
          </el-table-column>
          
          <el-table-column label="Chức năng" width="120">
            <template slot-scope="scope">
              <el-button @click="handleEdit(scope.row)" type="text" size="small"
                >{{$t('Button.Edit')}}</el-button
              >
              <el-button @click="handleDelete(scope.row.AccountID)" type="text" size="small"
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
          :total="accountTotal"
        >
        </el-pagination>
      </div>
    </div>
    <CreateAccount
      v-if="show"
      @closePopup="closePopup"
      :typePopup="typePopup"
      :account="account"
      ref="popup"
    />
  </div>
</template>

<script>
import { mapActions, mapState } from "vuex";
import CreateAccount from "./popup/create-account.vue";

export default {
  components: {
    CreateAccount,
  },
data() {
    return {
      search: "",
      currentPage: 1,
      pageSizes:[10, 20, 30, 40],
      pageSize:10,
      account: {},
      typePopup: true,
      show: false,
      loading:true
    };
  },
  computed: {
    ...mapState("account", ["accounts","accountTotal","pageTotal"]),
  },
  created() {
    const me = this;
    me.getAccounts(me.currentPage,me.pageSize,me.search)
  },
  methods: {
    ...mapActions("account", ["getAccountsPaging","deleteAccount",'updateAccount']),
    getAccounts(pageIndex,pageSize,queryText){
      const me=this
      const params={
        pageIndex:pageIndex,
        pageSize:pageSize,
        queryText:queryText
      }
       new Promise(resolve=>{
        me.getAccountsPaging(params);
        return resolve(true)
      }).then(res=>{
        me.loading=false
      })
    },
    /**
     * Sửa
     */
    handleEdit(account) {
      const me=this
      me.account = account;
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
          .deleteAccount(id)
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
              me.getAccounts(me.currentPage, me.pageSize, me.search);
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
      me.getAccounts(me.currentPage,me.pageSize,me.search)
    },
    handleCurrentChange(val) {
      const me=this
      me.currentPage=val
      me.getAccounts(me.currentPage,me.pageSize,me.search)
    },
    searchAccount() {
      const me = this;
      setTimeout(() => {
        me.currentPage = 1;
        me.pageSize = me.pageSizes[0];
        me.getAccounts(me.currentPage, me.pageSize, me.search);
      }, 500);
    },
    /**
     * Thêm mới
     */
    handleAdd(){
      const me=this
      me.account = {};
      me.typePopup = true;
      me.show=true
      me.$refs.popup.setModel(true);
    },
    closePopup() {
      const me = this;
      me.show = false;
    },
    handleChangeStatusValue(account){
      const me=this
      account.Status=!account.Status
      const params={
        id:account.AccountID,
        params:account
      }
      const h=me.$createElement
      me.updateAccount(params).then(res=>{
        me.$notify({
                title: me.$t("Notify.Success"),
                message: h(
                  "i",
                  { style: "color: teal" },
                  me.$t("Notify.UpdateSuccess",{object:"trạng thái tài khoản"})
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
    }
  },
}
</script>

<style lang="scss">
.account-list-container {
  //height: calc(100vh - 65px);
  .body {
    .grid {
      height: calc(100% - 49px - 17px);
      .status-btn{
        width: 80px;
    height: 30px;
    display: flex;
    align-items: center;
    justify-content: center;
    &.activated{
      background-color: rgb(65, 194, 65);
      color:white;
      font-weight: 600;
    }
    &.locked{
      background-color: rgb(235, 47, 47);
      color:white;
      font-weight: 600;
    }
      }
    }
  }
}

</style>
<style scoped>
.add-new-btn {
  @apply tw-bg-red-500 tw-text-white !important;
}
</style>
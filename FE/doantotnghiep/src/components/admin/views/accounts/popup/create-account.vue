<template>
  <el-dialog
    ref="dialog"
    :title="typePopup?'Thêm mới tài khoản':'Sửa tài khoản'"
    :visible.sync="modelDialog"
    @close="cancelFill"
  >
    <template #default>
      <el-form
        :model="modelForm"
        :rules="rules"
        ref="ruleForm"
        label-width="152px"
        label-position="left"
        class="demo-ruleForm"
      >
        <el-form-item
          class="tw-flex-1 tw-mr-4"
          :label="$t('Account.UserName')"
          prop="UserName"
        >
          <el-input
          :disabled="disableUserName"
            v-model="modelForm.UserName"
            :placeholder="$t('Account.UserName')"
          />
        </el-form-item>
        <el-form-item class="tw-flex-1 tw-mr-4" :label="$t('Account.Password')">
          <el-input
            v-model="modelForm.Password"
            :placeholder="$t('Account.Password')"
          />
        </el-form-item>
        <el-form-item class="tw-flex-1 tw-mr-4" :label="$t('Account.FullName')">
          <el-input
            v-model="modelForm.FullName"
            :placeholder="$t('Account.FullName')"
          />
        </el-form-item>
        <el-form-item class="tw-flex-1 tw-mr-4" :label="$t('Account.Email')">
          <el-input
            v-model="modelForm.Email"
            :placeholder="$t('Account.Email')"
          />
        </el-form-item>
        <el-form-item
          class="tw-flex-1 tw-mr-4"
          :label="$t('Account.PhoneNumber')"
        >
          <el-input
            v-model="modelForm.PhoneNumber"
            :placeholder="$t('Account.PhoneNumber')"
          />
        </el-form-item>
        <el-form-item class="tw-flex-1 tw-mr-4" :label="$t('Account.Status')">
          <el-radio-group v-model="modelForm.Permission">
          <el-radio :label="1">Quản trị</el-radio>
          <el-radio :label="0">Nhân viên</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item class="tw-flex-1 tw-mr-4" :label="$t('Account.Status')">
          <el-checkbox v-model="modelForm.Status" />
        </el-form-item>
      </el-form>
    </template>
    <template #footer>
      <div class="button">
        <el-button @click="completeFill" class="complete-btn">{{
          $t("Button.Complete")
        }}</el-button>
        <el-button @click="cancelFill" class="cancel-btn">{{
          $t("Button.Cancel")
        }}</el-button>
      </div>
    </template>
  </el-dialog>
</template>

<script>
import { mapState, mapActions } from "vuex";
export default {
  props: {
    account: {
      type: Object,
      default: () => {},
    },
    typePopup: {
      type: Boolean,
      default: true,
    },
  },
  data() {
    return {
      modelDialog: true,
      modelForm: {},
      preview: null,
      rules: {},
      pageIndex: 1,
      pageSize: 10,
      search: "",
    };
  },
  computed: {
    ...mapState("account", ["accounts","accountExists"]),
    disableUserName(){
      return !this.typePopup
    }
  },
  created() {
    const me = this;
    me.modelForm = JSON.parse(JSON.stringify(me.account));
  },
  methods: {
    ...mapActions("account", {
      insertAccount: "insertAccount",
      updateAccount: "updateAccount",
      getAccountsPaging: "getAccountsPaging",
      checkAccountExistsInDB:"checkAccountExistsInDB"
    }),
    setModel(value) {
      const me = this;
      me.modelDialog = value;
    },

    /**
     * Hoàn thành thêm thông tin chi tiết sản phẩm
     */
    async completeFill() {
      const me = this;
      if (this.typePopup) {
        const params = JSON.parse(JSON.stringify(me.modelForm));
        params.CreatedBy = JSON.parse(
          sessionStorage.getItem("Account")
        ).UserName;
        const h = this.$createElement;
        await me.checkAccountExistsInDB({userName:me.modelForm.UserName})
        if(me.accountExists.length>0){
          this.$notify.error({
              title: me.$t("Notify.Error"),
              message: h(
                "i",
                { style: "color: teal" },
                "Tài khoản đã tồn tại"
              ),
            });
        }else{
          await me
          .insertAccount(params)
          .then((res) => {
            if (res) {
              me.modelForm = {};
              
              this.$notify({
                title: me.$t("Notify.Success"),
                message: h(
                  "i",
                  { style: "color: teal" },
                  "Thêm tài khoản thành công"
                ),
                type: "success",
              });
              me.getAccounts(me.pageIndex, me.pageSize, me.search);
            }else{
              this.$notify.error({
              title: me.$t("Notify.Error"),
              message: h(
                "i",
                { style: "color: teal" },
                me.$t("Notify.HasError")
              ),
            });
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
        }
        me.modelDialog = false;
        
      } else {
        const params = JSON.parse(JSON.stringify(me.modelForm));
        params.ModifiedBy = JSON.parse(
          sessionStorage.getItem("Account")
        ).UserName;
        await me
          .updateAccount({ params: params, id: me.account.AccountID })
          .then(() => {
            me.modelForm = {};
            const h = this.$createElement;
            this.$notify({
              title: me.$t("Notify.Success"),
              message: h(
                "i",
                { style: "color: teal" },
                me.$t("Notify.UpdateSuccess",{object:'tài khoản'})
              ),
              type: "success",
            });
            me.getAccounts(me.pageIndex, me.pageSize, me.search);
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
          me.modelDialog = false;
      }

      
    },
    /**
     * Hủy thêm thôn gtin chi tiết sản phẩm
     */
    cancelFill() {
      const me = this;
      me.modelDialog = false;
      me.$emit("closePopup");
    },
    getAccounts(pageIndex, pageSize, queryText) {
      const me = this;
      const params = {
        pageIndex: pageIndex,
        pageSize: pageSize,
        queryText: queryText,
      };
      me.getAccountsPaging(params);
    },
  },
};
</script>

<style scoped>
.complete-btn {
  @apply tw-bg-red-500 tw-text-white !important;
}
</style>
<style lang="scss" scoped>
.image-area {
  .el-form-item__content {
    display: flex !important;
  }
}
::v-deep{
    .el-dialog{
        margin-top:8vh !important;
    }
}
</style>
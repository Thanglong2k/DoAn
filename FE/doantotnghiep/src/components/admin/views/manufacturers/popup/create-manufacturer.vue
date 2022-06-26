<template>
  <el-dialog
    ref="dialog"
    :title="typePopup?'Thêm mới hãng sản xuất':'Sửa hãng sản xuất'"
    :visible.sync="modelDialog"
    width="500px"
    @close="cancelFill"
  >
    <template #default>
      <el-form
        :model="modelForm"
        :rules="rules"
        ref="ruleForm"
        label-width="132px"
        label-position="left"
        class="demo-ruleForm"
      >
        <el-form-item
          class="tw-flex-1 tw-mr-4"
          :label="$t('Manufacturer.ManufacturerName')"
          prop="ManufacturerName"
        >
          <el-input
            v-model="modelForm.ManufacturerName"
            :placeholder="$t('Manufacturer.ManufacturerNameInput')"
          />
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
import { mapActions } from "vuex";
export default {
    props:{
        manufacturer:{
            type:Object,
            default:()=>{}
        },
        typePopup:{
            type:Boolean,
            default:true
        }
    },
  data() {
    return {
      modelDialog: false,
      modelForm: {},

      rules: {},
      pageIndex:1,
      pageSize:10,
      search:''
    };
  },
created(){
    const me=this
    me.modelForm=JSON.parse(JSON.stringify(me.manufacturer))
},
  methods: {
    ...mapActions("manufacturer", ["insertManufacturer","updateManufacturer","getManufacturersPaging"]),
    setModel(value) {
      const me = this;
      me.modelDialog = value;
    },
    /**
     * Hoàn thành thêm thông tin chi tiết sản phẩm
     */
    async completeFill() {
      const me = this;
      if(this.typePopup){

          const params = {
            ManufacturerName: me.modelForm.ManufacturerName,
            CreatedBy:JSON.parse(sessionStorage.getItem("Account")).UserName
          };
          const h = this.$createElement;
          await me.insertManufacturer(params).then((res) => {
              me.modelForm={}
              
            this.$notify({
              title: me.$t("Notify.Success"),
              message: h(
                "i",
                { style: "color: teal" },
                me.$t("Notify.InsertSuccess",{object:'hãng sản xuất'})
              ),
              type: "success",
            });
            me.getManufacturers(me.pageIndex, me.pageSize, me.search);
          }).catch(()=>{
              
    
                this.$notify.error({
                  title: me.$t("Notify.Error"),
                  message: h(
                    "i",
                    { style: "color: teal" },
                    me.$t("Notify.HasError")
                  ),
                });
          });
      }else{
          const params = {
            ManufacturerName: me.modelForm.ManufacturerName,
            ModifiedBy:JSON.parse(sessionStorage.getItem("Account")).UserName
          };
          const h = this.$createElement;
          await me.updateManufacturer({params:params,id:me.manufacturer.ManufacturerID}).then((res) => {
              me.modelForm={}
              
            this.$notify({
              title: me.$t("Notify.Success"),
              message: h(
                "i",
                { style: "color: teal" },
                me.$t("Notify.UpdateSuccess",{object:'hãng sản xuất'})
              ),
              type: "success",
            });
            me.getManufacturers(me.pageIndex, me.pageSize, me.search);
          }).catch(()=>{
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
    },
    /**
     * Hủy thêm thôn gtin chi tiết sản phẩm
     */
    cancelFill() {
      const me = this;
      me.modelDialog = false;
      me.$emit('closePopup')
    },
    getManufacturers(pageIndex, pageSize, queryText) {
      const me = this;
      const params = {
        pageIndex: pageIndex,
        pageSize: pageSize,
        queryText: queryText,
      };
      me.getManufacturersPaging(params);
    },
  },
};
</script>

<style scoped>
.complete-btn {
  @apply tw-bg-red-500 tw-text-white !important;
}
</style>
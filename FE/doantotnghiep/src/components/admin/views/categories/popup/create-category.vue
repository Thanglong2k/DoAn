<template>
  <el-dialog
    ref="dialog"
    :title="typePopup?'Thêm mới danh mục sản phẩm':'Sửa danh mục sản phẩm'"
    :visible.sync="modelDialog"
    width="500px"
    @close="cancelFill"
  >
    <template #default>
      <el-form
        :model="modelForm"
        :rules="rules"
        ref="ruleForm"
        label-width="130px"
        label-position="left"
        class="demo-ruleForm"
      >
        <el-form-item
          class="tw-flex-1 tw-mr-4"
          :label="$t('Category.CategoryName')"
          prop="CategoryName"
        >
          <el-input
            v-model="modelForm.CategoryName"
            :placeholder="$t('Category.CategoryNameInput')"
          />
        </el-form-item>
        <el-form-item
            class="tw-flex-1 tw-mr-4"
            :label="$t('Category.ParentCategory')"
          >
            <el-select
            class="tw-w-full"
              v-model="modelForm.CategoryParentID"
              :placeholder="$t('Category.ParentCategory')"
            >
              <el-option
                v-for="item in optionCategories"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              >
              </el-option>
            </el-select>
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
    props:{
        category:{
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
      modelDialog: true,
      modelForm: {},
      preview:null,
      rules: {},
      pageIndex:1,
      pageSize:10,
      search:'',
      parentCategory:''
    };
  },
  computed:{
    ...mapState("category", ["categories"]),
    /**
     * Giá trị option chọn hãng sản xuất
     */
    optionCategories() {
      const me = this;
      return me.categories?.map((item) => {
        return { value: item.CategoryID, label: item.CategoryName };
      });
    },
  },
created(){
    const me=this
    me.modelForm=JSON.parse(JSON.stringify(me.category))
    me.getParentCategories()
},
  methods: {
    ...mapActions("category", {insertCategory:"insertCategory",updateCategory:"updateCategory",getCategoriesPaging:"getCategoriesPaging",getParentCategories:"getCategories"}),
    setModel(value) {
      const me = this;
      me.modelDialog = value;
    },
    selectImage() {
      this.$refs.file.click();
    },
    previewImage(event) {
      var input = event.target;

      if (input.files) {
        var reader = new FileReader();
        reader.onload = (e) => {
          this.preview=e.target.result;
        };

        this.image = input.files[0];
        reader.readAsDataURL(this.image);
      }
    },
    /**
     * Hoàn thành thêm thông tin chi tiết sản phẩm
     */
    async completeFill() {
      const me = this;
      if(this.typePopup){

          const params = JSON.parse(JSON.stringify(me.modelForm))
          params.CreatedBy=JSON.parse(sessionStorage.getItem("Account")).UserName
          const h = this.$createElement;
          await me.insertCategory(params).then(() => {
              me.modelForm={}
              
            this.$notify({
              title: me.$t("Notify.Success"),
              message: h(
                "i",
                { style: "color: teal" },
                me.$t("Notify.UpdateSuccess",{object:'danh mục'})
              ),
              type: "success",
            });
            me.getCategories(me.pageIndex,me.pageSize,me.search)
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
          const params = JSON.parse(JSON.stringify(me.modelForm))
          params.ModifiedBy=JSON.parse(sessionStorage.getItem("Account")).UserName
          const h = this.$createElement;
          await me.updateCategory({params:params,id:me.category.CategoryID}).then(() => {
              me.modelForm={}
            this.$notify({
              title: me.$t("Notify.Success"),
              message: h(
                "i",
                { style: "color: teal" },
                me.$t("Notify.InsertSuccess",{object:'danh mục'})
              ),
              type: "success",
            });
            me.getCategories(me.pageIndex,me.pageSize,me.search)
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
    getCategories(pageIndex,pageSize,queryText){
      const me=this
      const params={
        pageIndex:pageIndex,
        pageSize:pageSize,
        queryText:queryText
      }
      me.getCategoriesPaging(params);
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
.image-area{
  .el-form-item__content{
    display:flex !important;
  }
}
</style>
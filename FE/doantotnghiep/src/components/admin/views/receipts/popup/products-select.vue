<template>
  <el-dialog
  class="dialog-container"
    ref="dialog"
    title="Chọn sản phẩm"
    width="600px"
    :lock-scroll="false"
    :visible.sync="modelDialog"
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
          v-for="(product,index) in products"
:key="index"
        >
          <div class="product-item tw-flex tw-justify-between tw-w-full">
              <div class="left tw-flex">

                <div class="product-image">
                    <img :src="product.Product.Avatar" width="100px" height="100px"/>
                </div>
                <div class="product-name">{{product.Product.ProductName}}</div>
                
              </div>
              <div class="right tw-flex">
                  <div class="product-memory tw-w-28 tw-text-center tw-font-semibold">{{product.ProductAttribute.Memory+' GB'}}</div>
                <el-button class="function tw-h-10" v-if="indexSelectedProducts.includes(product.ProductAttribute.ProductAttributeID)" @click="cancelSelectProduct(product)">Bỏ chọn</el-button>
                <el-button class="function tw-h-10" v-else @click="selectProduct(product)">Chọn</el-button>
              </div>
          </div>
        </el-form-item>
        
          
      </el-form>
      
    </template>
    <template #footer>
        <div class="paging">
            <el-pagination
          class="tw-flex tw-justify-end tw-h-12 tw-items-center"
          @size-change="handleSizeChange"
          @current-change="handleCurrentChange"
          :current-page.sync="pageIndex"
          :page-size="pageSize"
          :page-sizes="pageSizes"
          layout="total,sizes, prev, pager, next"
          :total="productTotal"
        >
        </el-pagination>
        </div>
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
import {mapState, mapActions, mapMutations} from 'vuex'
export default {
data() {
    return {
      modelDialog: false,
      modelForm: {},
      preview:null,
      rules: {},
      pageIndex:1,
      pageSize:10,
      pageSizes:[10, 20, 30, 40],
      search:'',
      selectedProducts:[],
      indexSelectedProducts:[],
    };
  },
  computed:{
      ...mapState('product',['products','productTotal','pageTotal'])
  },
  created(){
      const me=this
      
      me.getProducts(me.pageIndex,me.pageSize)
  },
  methods:{
      ...mapActions('product',['getProductAndProductAttribute']),
      setModel(value) {
      const me = this;
      me.modelDialog = value;
    },
    getProducts(pageIndex,pageSize){
      const me=this
      const params={
        pageIndex:pageIndex,
        pageSize:pageSize
      }
      me.getProductAndProductAttribute(params);
    },
    handleSizeChange(val) {
      const me=this
      me.pageSize=val
      me.getProducts(me.pageIndex,me.pageSize)
    },
    handleCurrentChange(val) {
      const me=this
      me.currentPage=val
      me.getProducts(me.pageIndex,me.pageSize)
    },
    selectProduct(product){
        const me=this
        
        me.selectedProducts.push(product)
        me.indexSelectedProducts.push(product.ProductAttribute.ProductAttributeID)
    },
    cancelSelectProduct(product){
        const me=this
        
        me.selectedProducts=me.selectedProducts.filter(item=>{
            return item.ProductAttribute.ProductID!==product.ProductAttribute.ProductID||item.ProductAttribute.ProductAttributeID!==product.ProductAttribute.ProductAttributeID
        })
        var indexItem = me.indexSelectedProducts.indexOf(product.ProductAttribute.ProductAttributeID);
        me.indexSelectedProducts.splice(indexItem,1)
    },
    completeFill(){
        const me=this
        me.$emit("setSelectedProducts",me.selectedProducts)
    },
    cancelFill(){
      const me=this

      me.selectedProducts=[]
      me.indexSelectedProducts=[]
      me.modelDialog=false
    }
  }
}
</script>

<style lang="scss" scoped>
::v-deep{
    .el-dialog{
        margin-top:5vh !important;
        &__body{
            height:560px;
            overflow: auto;
        }
    }
    .el-form-item__content{
        margin-left: 0px !important;
    }
}
.dialog-container{
    
}
</style>
<template>
  <div class="imei-container tw-p-6">
      <div class="button-area">
          <el-button @click="openProductIMEIs">Thêm IMEI</el-button>
      </div>
      <div class="product-info tw-flex tw-mt-6">
        
        <div class="product-name tw-mr-10 tw-flex">
          <div class="label-text">Tên sản phẩm:</div>
          <div class="value-item tw-text-xl">{{productName}}</div>
          
        </div>
        <div class="product-img tw-flex">
          <div class="label-text">Ảnh:</div>
          <div class="value-item"><img width="100px" height="100px" :src="productImg"/></div> 
        </div>

      </div>
      <div class="imei-list tw-mt-6"
      style="height:calc(100% - 130px)">
          <el-table
          class="table"
          :data="productDetails"
          height="calc(100% - 75px)"
          style="width: 100%"
        >
          <!-- <el-table-column prop="Product.ProductName" label="Tên sản phẩm" min-width="150"> </el-table-column>
          <el-table-column  label="Ảnh" width="100">
              <template slot-scope="scope">
                  <img :src="scope.row.Product.Avatar"/>
              </template>
          </el-table-column> -->
          <el-table-column prop="ProductAttribute.Memory" label="Bộ nhớ" width="150">
              <template slot-scope="scope">
                  {{scope.row.ProductAttribute.Memory + " GB"}}
              </template>
          </el-table-column>
          <el-table-column prop="ProductDetail.DetailNumber" label="Sản phẩm thứ" width="150"> </el-table-column>
          <el-table-column prop="ProductDetail.IMEI" label="IMEI" min-width="150">
          </el-table-column>
          <el-table-column label="Chức năng" width="120">
            <template slot-scope="scope">
              <el-button @click="handleEdit(scope.row.ProductDetailID)" type="text" size="small"
                >{{$t('Button.Edit')}}</el-button
              >
              <el-button @click="handleDelete(scope.row.ProductDetailID)" type="text" size="small"
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
          :total="productDetailTotal"
        >
        </el-pagination>
      </div>
      <ProductDetail ref="popup" :productAttribute="uniqueProductAttribute()"/>   
  </div>
</template>

<script>
import { mapActions, mapState } from "vuex";
import ProductDetail from './product-detail.vue'
export default {
  components:{
    ProductDetail
  },
    data() {
    return {
      search: "",
      currentPage: 1,
      pageSizes:[10, 20, 30, 40],
      pageSize:10,
      listLoading: true,
      downloadLoading: false,
      filename: 'Danh sách sản phẩm',
      autoWidth: true,
      bookType: 'xlsx'
    };
  },
  computed: {
    ...mapState("product", ["productDetails","productDetailTotal","pageTotal"]),
    productName(){
      return this.productDetails[0]?.Product?.ProductName
    },
    productImg(){
      return this.productDetails[0]?.Product?.Avatar
    },
    
  },
  created() {
    const me = this;
    me.getProductDetails(me.currentPage,me.pageSize,me.search)
  },
    methods:{
        ...mapActions("product", ["getProductDetailsPaging","deleteProductDetail"]),
    getProductDetails(pageIndex,pageSize,queryText){
      const me=this
      const params={
        pageIndex:pageIndex,
        pageSize:pageSize,
        queryText:queryText,
        productID:me.$route.query.productID

      }
      me.getProductDetailsPaging(params);
    },
        /**
     * Sửa sản phẩm
     */
    handleEdit(productID) {
      const me=this
      me.$router.push(`product/create-product?productID=${productID}`)
    },
    /**
     * Xóa sản phẩm
     */
    async handleDelete(productID) {
      const me=this

      await me.$confirm('Bạn có chắc chắn muốn xóa chi tiết sản phẩm này?', 'Cảnh báo', {
          confirmButtonText: 'Đồng ý',
          cancelButtonText: 'Hủy',
          type: 'warning'
        }).then(async () => {
          
          const h = this.$createElement;
          await me.deleteProductDetail(productID).then(res=>{

            if(res){
              me.$notify({
                  title: me.$t("Notify.Success"),
                  message: h(
                    "i",
                    { style: "color: teal" },
                    me.$t("Notify.DeleteProductSuccess")
                  ),
                  type: "success",
                });
                
            }else{
              me.$notify({
                  title: me.$t("Notify.Error"),
                  message: h(
                    "i",
                    { style: "color: teal" },
                    me.$t("Notify.HasError")
                  ),
                  type: "error",
                });
            }
          }).catch(e=>{
            me.$notify({
                  title: me.$t("Notify.Error"),
                  message: h(
                    "i",
                    { style: "color: teal" },
                    me.$t("Notify.HasError")
                  ),
                  type: "error",
                });
            
          })
          
        })
        await  me.getProductDetails(me.currentPage,me.pageSize,me.search)
    },
    handleSizeChange(val) {
      const me=this
      me.pageSize=val
      me.getProductDetails(me.currentPage,me.pageSize,me.search)
    },
    handleCurrentChange(val) {
      const me=this
      me.currentPage=val
      me.getProductDetails(me.currentPage,me.pageSize,me.search)
    },
    searchProduct() {
      const me=this
      setTimeout(me.getProductDetails(me.currentPage,me.pageSize,me.search),500)
    },
    openProductIMEIs(){
      const me=this
      me.$refs.popup.setModel(true)
    },
    productAttribute(){
      const me=this
      const productAttributes=me.productDetails.map(item=>{
        return item.ProductAttribute
      })
      const t=me.$commonFn.unique(productAttributes)
      return t
    },
    uniqueProductAttribute(){
      const me=this
      const productAttributes=me.productDetails.map(item=>{
        return item.ProductAttribute
      })
      const productAttributeIDs= productAttributes.map(item=>{
        return item.ProductAttributeID
      })
      let uniqueProductAttributeIDs=me.$commonFn.unique(productAttributeIDs)
      let productAttributeFinal=[]
      const idPushed=[]
      
        productAttributes.forEach((item,index)=>{
          if(uniqueProductAttributeIDs.includes(item.ProductAttributeID)&& idPushed.indexOf(item.ProductAttributeID)<0){
            productAttributeFinal.push(item)
            idPushed.push(item.ProductAttributeID)
          }
        })
      return productAttributeFinal
    }
    },
}
</script>

<style lang="scss" scoped>
.imei-container{
  .product-info{
    .label-text{
      font-weight: 600;
      margin-right:24px;
    }
  }
}
</style>
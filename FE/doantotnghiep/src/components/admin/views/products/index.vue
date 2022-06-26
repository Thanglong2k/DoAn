<template>
  <div class="product-list-container tw-w-full">
    <div class="body tw-h-full">
      <div class="filter tw-mx-4 tw-h-12 tw-flex tw-justify-between">
        <div class="search tw-w-3/4">
          <el-input
            v-model="search"
            @input="searchProduct"
            :placeholder="$t('Search')"
          />
        </div>
        <el-button class="add-new-btn" @click="addProduct">{{$t('Button.AddNew')}}</el-button>
        <el-button class="add-new-btn" @click="handleDownload">Xuất Excel</el-button>
      </div>
      <div class="grid tw-p-5 tw-pb-0 tw-mt-4">
        <el-table
          class="table"
          :data="products"
          height="calc(100% - 49px - 16px)"
          style="width: 100%"
        >
          <el-table-column type="index" width="50"> </el-table-column>
          <el-table-column prop="ProductName" label="Sản phẩm" min-width="150">
          </el-table-column>
          <el-table-column label="Ảnh"  align="center" width="120">
            <template slot-scope="scope">
              <div class="image tw-w-20 tw-h-20">
                <el-image :src="scope.row.Avatar" fit="contain">
                  <div slot="placeholder" class="image-slot">
                    {{$t('Common.Loading')}}<span class="dot">...</span>
                  </div>
                </el-image>
              </div>
            </template>
          </el-table-column>
          <el-table-column prop="Price" label="Giá" align="right" width="120">
            <template slot-scope="scope">
              {{$commonFn.formatMoney2(scope.row.Price.toString())}}
            </template>
          </el-table-column>
          <el-table-column prop="Quantity" label="Số lượng"  align="right" width="120">
          </el-table-column>
          <el-table-column prop="Monitor" label="Màn hình" width="300">
          </el-table-column>
          <el-table-column prop="Ram" label="Ram"  align="right" width="120">
          </el-table-column>
          <el-table-column label="Chức năng" width="120">
            <template slot-scope="scope">
              <el-button @click="handleEdit(scope.row.ProductID)" type="text" size="small"
                >{{$t('Button.Edit')}}</el-button
              >
              <el-button @click="handleDelete(scope.row.ProductID)" type="text" size="small"
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
          :total="productTotal"
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
      listLoading: true,
      downloadLoading: false,
      filename: 'Danh sách sản phẩm',
      autoWidth: true,
      bookType: 'xlsx'
    };
  },
  computed: {
    ...mapState("product", ["products","productTotal","pageTotal"]),
  },
  created() {
    const me = this;
    me.getProducts(me.currentPage,me.pageSize,me.search)
  },
  methods: {
    ...mapActions("product", ["getProductsPaging","deleteProduct"]),
    getProducts(pageIndex,pageSize,queryText){
      const me=this
      const params={
        pageIndex:pageIndex,
        pageSize:pageSize,
        queryText:queryText
      }
      me.getProductsPaging(params);
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

      await me.$confirm('Bạn có chắc chắn muốn xóa sản phẩm này?', 'Cảnh báo', {
          confirmButtonText: 'Đồng ý',
          cancelButtonText: 'Hủy',
          type: 'warning'
        }).then(async () => {
          
          const h = this.$createElement;
          await me.deleteProduct(productID).then(res=>{

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
        await  me.getProducts(me.currentPage,me.pageSize,me.search)
    },
    handleSizeChange(val) {
      const me=this
      me.pageSize=val
      me.getProducts(me.currentPage,me.pageSize,me.search)
    },
    handleCurrentChange(val) {
      const me=this
      me.currentPage=val
      me.getProducts(me.currentPage,me.pageSize,me.search)
    },
    searchProduct() {
      const me=this
      setTimeout(me.getProducts(me.currentPage,me.pageSize,me.search),500)
    },
    /**
     * Thêm mới sản phẩm
     */
    addProduct(){
      const me=this
      me.$router.push("product/create-product?create=true")
    },
    handleDownload() {
      this.downloadLoading = true
      import('@/vendor/Export2Excel').then(excel => {
        const tHeader = ['Sản phẩm', 'ảnh', 'giá', 'số lượng', 'màn hình','ram']
        const filterVal = ['ProductName', 'Avatar', 'Price', 'Quantity', 'Monitor', 'Ram']
        const list = this.products
        const data = this.formatJson(filterVal, list)
        excel.export_json_to_excel({
          header: tHeader,
          data,
          filename: this.filename,
          autoWidth: this.autoWidth,
          sheetname:this.filename,
          bookType: this.bookType
        })
        this.downloadLoading = false
      })
    },
    formatJson(filterVal, jsonData) {
      return jsonData.map(v => filterVal.map(j => {
        
          return v[j]
        
      }))
    }
  },
};
</script>

<style lang="scss" scoped>
.product-list-container {
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
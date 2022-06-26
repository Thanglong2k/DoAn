<template>
  <el-dialog
    ref="dialog"
    :title="title"
    :visible.sync="modelDialog"
  >
    <template #default>
        <div class="images">

            <ProductCarousel :imageProduct="image"/>
        </div>
        <div class="product-info">
            <div class="form-header">Thông tin sản phẩm</div>
            <div class="tw-grid tw-grid-cols-2">
                <div class="attribute">
                    <div class="name">Tên sản phẩm</div>
                    <div class="trademark">Thương hiệu</div>
                    <div class="release-time">Thời gian ra mắt</div>
                    <div class="warranty">Thời gian bảo hành (tháng)</div>
                    <div class="origin">Xuất xứ</div>
                    <div class="origin">Hướng dẫn bảo quản</div>
                    <div class="origin">Hướng dẫn sử dụng</div>
                </div>
                <div class="specification-info">
                    <div class="name">{{product.ProductName}}</div>
                    <div class="trademark">{{manufacturer.ManufacturerName}}</div>
                    <div class="release-time">{{product.ReleaseTime}}</div>
                    <div class="warranty">{{product.Warranty}}</div>
                    <div class="origin">{{product.Origin}}</div>
                    <div class="origin">Để nơi khô ráo, nhẹ tay, dễ vỡ</div>
                    <div class="origin">Xem trong sách hướng dẫn sử dụng</div>
                </div>
                
            </div>
        </div>
        <div class="specifications" v-if="product.Type===$enumeration.ProductType.Phone">
            <div class="form-header">Đặc tính</div>
            <div class="tw-grid tw-grid-cols-2">

                <div class="attribute">
                    <div class="monitor">Màn hình</div>
                    <div class="gpu">GPU</div>
                    <div class="ram">Ram</div>
                    <div class="memory">Memory</div>
                    <div class="camera">Camera</div>
                    <div class="battery">Battery</div>
                    <div class="weight">Weight</div>
                    <div class="size">Size</div>
                    <div class="operating-system">OperatingSystem</div>
                    
        
                </div>
                <div class="specification-info">
                    <div class="monitor">{{product.Monitor}}</div>
                    <div class="gpu">{{product.GPU}}</div>
                    <div class="ram">{{product.Ram}}</div>
                    <div class="memory">{{productSelected.Memory}}</div>
                    <div class="camera">{{product.Camera}}</div>
                    <div class="battery">{{product.Battery}}</div>
                    <div class="weight">{{product.Weight}}</div>
                    <div class="size">{{product.Size}}</div>
                    <div class="operating-system">{{product.OperatingSystem}}</div>
                </div>
            </div>
            
        </div>
    </template>
    
  </el-dialog>
</template>

<script>
import ProductCarousel from "@/components/controls/product-carousel";
import {mapActions, mapState} from 'vuex'
export default {
    props:{
        product:{
            type:Object,
            default:()=>{}
        },
        productSelected:{
            type:Object,
            default:()=>{}
        },
        title:{
            type:String,
            default:''
        },
        manufacturer:{
            type:Object,
            default:()=>{}
        }
    },
    components:{
        ProductCarousel
    },
    data(){
        return{
            modelDialog:false
        }
    },
    computed:{
        // ...mapState('manufacturer',['manufacturer']),
        image(){
            return this.product?.Image.split(',')
        }
    },
    methods:{
        ...mapActions('manufacturer',['getManufacturerByID']),
        //lấy tên hãng sản xuất
    //   getManufacturer(manufacturerID){
    //       const me=this
    //       debugger
    //       me.getManufacturerByID(manufacturerID)
          
    //       return me.manufacturer.ManufacturerName
    //   },
      setModelDialog(){
          const me=this
          me.modelDialog=true
      }
    }
    
}
</script>

<style lang="scss" scoped>
@import "@/styles/style.scss";
.product-info,.specifications{
    line-height:32px;
    .form-header{
        background-color: $gray-30;
                color:$gray-title;
                font-weight: 600;
                font-size: 20px;
                line-height: 24px;
                padding:12px 0;
    }
}

</style>
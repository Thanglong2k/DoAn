<template>
  <div class="compare-data-container">
    <el-collapse v-model="activeNames" @change="handleChange">
      <el-collapse-item title="Thông tin sản phẩm" name="1">
        <div class="tw-grid collapse-item">
            <div class="attribute">
                <div class="name">Tên sản phẩm</div>
                <div class="trademark">Thương hiệu</div>
                <div class="release-time">Thời gian ra mắt</div>
                <div class="warranty">Thời gian bảo hành</div>
                <div class="origin">Xuất xứ</div>
            </div>
            <div class="product-1" v-if="compareProducts.length>0">
                <div class="name">{{compareProducts[0].ProductName}}</div>
                <div class="trademark">{{manufacturers.length>0?manufacturers[0].ManufacturerName:'-'}}</div>
                <div class="release-time">{{compareProducts[0].ReleaseTime}}</div>
                <div class="warranty">{{compareProducts[0].Warranty}}</div>
                <div class="origin">{{compareProducts[0].Origin}}</div>
            </div>
            <div class="product-2" v-if="compareProducts.length>1">
                <div class="name">{{compareProducts[1].ProductName}}</div>
                <div class="trademark">{{manufacturers.length>1?manufacturers[1].ManufacturerName:'-'}}</div>
                <div class="release-time">{{compareProducts[1].ReleaseTime}}</div>
                <div class="warranty">{{compareProducts[1].Warranty}}</div>
                <div class="origin">{{compareProducts[1].Origin}}</div>
            </div>
        </div>
      </el-collapse-item>
      <el-collapse-item title="Đặc tính" name="2">
        <div class="tw-grid collapse-item">
            <div class="attribute">
                <div class="monitor">Màn hình</div>
                <div class="gpu">GPU</div>
                <div class="ram">Ram</div>
                <div class="memory">Dung lượng</div>
                <div class="camera">Camera</div>
                <div class="battery">Pin</div>
                <div class="weight">Trọng lượng</div>
                <div class="size">Kích thước</div>
                <div class="operating-system">Hệ điều hành</div>
                
    
            </div>
            <div class="product-1" v-if="compareProducts.length>0">
                <div class="monitor">{{compareProducts[0].Monitor?compareProducts[0].Monitor:'-'}}</div>
                <div class="gpu">{{compareProducts[0].GPU?compareProducts[0].GPU:'-'}}</div>
                <div class="ram">{{compareProducts[0].Ram?compareProducts[0].Ram:'-'}}</div>
                <div class="memory">{{compareProducts[0].ProductAttributes[0].Memory?compareProducts[0].ProductAttributes[0].Memory:'-'}}</div>
                <div class="camera">{{compareProducts[0].Camera?compareProducts[0].Camera:'-'}}</div>
                <div class="battery">{{(compareProducts[0].Battery&&compareProducts[0].BatteryType)?compareProducts[0].Battery + compareProducts[0].BatteryType:'-'}}</div>
                <div class="weight">{{compareProducts[0].Weight?compareProducts[0].Weight:'-'}}</div>
                <div class="size">{{compareProducts[0].Size?compareProducts[0].Size:'-'}}</div>
                <div class="operating-system">{{compareProducts[0].OperatingSystem?compareProducts[0].OperatingSystem:'-'}}</div>
            </div>
            <div class="product-2" v-if="compareProducts.length>1">
                <div class="monitor">{{compareProducts[1].Monitor?compareProducts[1].Monitor:'-'}}</div>
                <div class="gpu">{{compareProducts[1].GPU?compareProducts[1].GPU:'-'}}</div>
                <div class="ram">{{compareProducts[1].Ram?compareProducts[1].Ram:'-'}}</div>
                <div class="memory">{{compareProducts[1].ProductAttributes[0].Memory?compareProducts[1].ProductAttributes[0].Memory:'-'}}</div>
                <div class="camera">{{compareProducts[1].Camera?compareProducts[1].Camera:'-'}}</div>
                <div class="battery">{{(compareProducts[1].Battery&&compareProducts[1].BatteryType)?compareProducts[1].Battery + compareProducts[1].BatteryType:'-'}}</div>
                <div class="weight">{{compareProducts[1].Weight?compareProducts[1].Weight:'-'}}</div>
                <div class="size">{{compareProducts[1].Size?compareProducts[1].Size:'-'}}</div>
                <div class="operating-system">{{compareProducts[1].OperatingSystem?compareProducts[1].OperatingSystem:'-'}}</div>
            </div>
        </div>
      </el-collapse-item>
    </el-collapse>
  </div>
</template>

<script>
import {mapActions, mapMutations, mapState} from 'vuex'
export default {
    props:{
        compareProducts:{
            type:Array,
            default:()=>[]
        },
        manufacturers:{
            type:Array,
            default:()=>[]
        }
    },
    data() {
      return {
        activeNames: ['1','2'],
        newProducts:JSON.parse(JSON.stringify(this.compareProducts))
      };
    },
    computed:{
        ...mapState('manufacturer',['manufacturer']),
        compare(){
            const t=this.compareProducts
            
            return t
        }
    },
    mounted(){
        const me=this
        
        
        
        // me.newProducts=me.compareProducts.map(item=>{
        //     for(let attribute of item){
        //         if(!attribute){
        //             attribute="-"
        //         }
        //     }
        //     return item
        // })
    },
    methods: {
        ...mapActions('manufacturer',['getManufacturerByID']),
      handleChange(val) {
        console.log(val);
      },
      //lấy tên hãng sản xuất
    //   getManufacturer(manufacturerID){
    //       const me=this
    //       me.getManufacturerByID(manufacturerID)
          
    //       return me.manufacturer.ManufacturerName
    //   }
    },
    
};
</script>

<style lang="scss" scoped>
@import "@/styles/style.scss";
::v-deep{
    .el-collapse{
        
        .el-collapse-item{
    
            &__header{
                background-color: $gray-30;
                color:$gray-title;
                font-weight: 600;
                font-size: 20px;
                line-height: 24px;
                padding:0 0 0 24px;
            }
            &__wrap{
                padding:0 24px;
            }
        }
    }
}
.compare-data-container{
    .collapse-item{
        grid-template-columns: 20% 40% 40%;
        line-height:32px;
        margin-top: 12px;
    }
}
</style>
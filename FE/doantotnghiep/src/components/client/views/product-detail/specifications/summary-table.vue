<template>
<div class="specifications-summary-table tw-w-full">
    <table class="tw-w-full">
        <tr class="tw-w-full" v-if="product.Type===$enumeration.ProductType.Phone">
            <td class="tw-w-2/5">{{$t('Product.Monitor')}}</td>
            <td class="tw-w-3/5">{{product.Monitor}}</td>
        </tr>
        <tr class="tw-w-full" v-if="product.Type===$enumeration.ProductType.Phone">
            <td class="tw-w-2/5">{{$t('Product.Ram')}}</td>
            <td class="tw-w-3/5">{{product.Ram}}</td>
        </tr>
        <tr class="tw-w-full" v-if="product.Type===$enumeration.ProductType.Phone">
            <td class="tw-w-2/5">{{$t('Product.Memory')}}</td>
            <td class="tw-w-3/5">{{productSelected.Memory}}</td>
        </tr>
        <tr class="tw-w-full" v-if="product.Type===$enumeration.ProductType.Phone">
            <td class="tw-w-2/5">{{$t('Product.Battery')}}</td>
            <td class="tw-w-3/5">{{product.Battery}}</td>
        </tr>
        <tr class="tw-w-full">
            <td class="tw-w-2/5">{{$t('Product.Origin')}}</td>
            <td class="tw-w-3/5">{{product.Origin}}</td>
        </tr>
        <tr class="tw-w-full">
            <td class="tw-w-2/5">{{$t('Product.ReleaseTime')}}</td>
            <td class="tw-w-3/5">{{product.ReleaseTime}}</td>
        </tr>
        <tr class="tw-w-full" v-if="product.Type===$enumeration.ProductType.Phone">
            <td class="tw-w-2/5">{{$t('Product.OperatingSystem')}}</td>
            <td class="tw-w-3/5">{{product.OperatingSystem}}</td>
        </tr>
    </table>
</div>
</template>

<script>
export default {
    props:{
        product:{
            type:Object,
            default:()=>{}
        },
        productSelected:{
            type:Object,
            default:()=>{}
        }
    }
}
</script>

<style lang="scss" scoped>
.specifications-summary-table{
    table{
        border-collapse: collapse;
        
        tr{
            &:nth-child(odd){
                background:#f3f4f7 ;
            }
            td{
                border: 1px solid #dee2e6;
                padding:4px 12px;
            }
        }
    }
}
</style>
<template>
  <div class="filter-product-container tw-bg-white tw-p-4">
    <div class="manufacturer tw-mb-3" v-if="isSearch|| isAccessory">
      <div class="filter-type tw-text-base">Hãng sản xuất</div>
      <div class="property">
        <v-checkbox
          class="my-checkbox"
          label="Tất cả"
          color="red"
          :input-value="isCheckAllManufacturerclone"
          hide-details
          @change="checkAll(filterType.Manufacturer)"
        ></v-checkbox>
        <v-checkbox
          class="my-checkbox"
          v-for="item in manufacturers"
          :key="item.ManufacturerID"
          v-model="checkedValueManufacturer"
          :label="item.ManufacturerName"
          color="red"
          :value="item.ManufacturerID"
          hide-details
          @change="onCheckedItem(filterType.Manufacturer)"
        ></v-checkbox>
      </div>
    </div>
    <div class="price tw-mb-3">
      <div class="filter-type tw-text-base">Mức giá</div>
      <div class="property">
        <v-checkbox
          class="my-checkbox"
          label="Tất cả"
          color="red"
          :input-value="isCheckAllPrice"
          hide-details
          @change="checkAll(filterType.Price)"
        ></v-checkbox>
        <v-checkbox
          class="my-checkbox"
          label="Dưới 2 triệu"
          color="red"
          v-model="checkedValuePrice"
          :value="priceValue.LessThanTwo"
          hide-details
          @change="onCheckedItem(filterType.Price)"
        ></v-checkbox>
        <v-checkbox
          class="my-checkbox"
          label="Từ 2-4 triệu"
          color="red"
          v-model="checkedValuePrice"
          :value="priceValue.TwoToFour"
          hide-details
          @change="onCheckedItem(filterType.Price)"
        ></v-checkbox>
        <v-checkbox
          class="my-checkbox"
          label="Từ 4-7 triệu"
          color="red"
          v-model="checkedValuePrice"
          :value="priceValue.FourToSeven"
          hide-details
          @change="onCheckedItem(filterType.Price)"
        ></v-checkbox>
        <v-checkbox
          class="my-checkbox"
          label="Từ 7-15 triệu"
          color="red"
          v-model="checkedValuePrice"
          :value="priceValue.SevenToFifteen"
          hide-details
          @change="onCheckedItem(filterType.Price)"
        ></v-checkbox>
        <v-checkbox
          class="my-checkbox"
          label="Trên 15 triệu"
          color="red"
          v-model="checkedValuePrice"
          :value="priceValue.OverFifteen"
          hide-details
          @change="onCheckedItem(filterType.Price)"
        ></v-checkbox>
      </div>
    </div>
    <div class="monitor tw-mb-3" v-if="!isAccessory">
      <div class="filter-type tw-text-base">Màn hình</div>
      <div class="property">
        <v-checkbox
          class="my-checkbox"
          label="Tất cả"
          color="red"
          :input-value="isCheckAllMonitor"
          hide-details
          @change="checkAll(filterType.Monitor)"
        ></v-checkbox>
        <v-checkbox
          class="my-checkbox"
          label="Dưới 6.0 inch"
          color="red"
          v-model="checkedValueMonitor"
          :value="monitorValue.LessThanSix"
          hide-details
          @change="onCheckedItem(filterType.Monitor)"
        ></v-checkbox>
        <v-checkbox
          class="my-checkbox"
          label="Trên 6.0 inch"
          color="red"
          v-model="checkedValueMonitor"
          :value="monitorValue.OverSix"
          hide-details
          @change="onCheckedItem(filterType.Monitor)"
        ></v-checkbox>
      </div>
    </div>
    <div class="battery tw-mb-3" v-if="!isAccessory">
      <div class="filter-type tw-text-base">Dung lượng pin</div>
      <div class="property">
        <v-checkbox
          class="my-checkbox"
          label="Tất cả"
          color="red"
          :input-value="isCheckAllBattery"
          hide-details
          @change="checkAll(filterType.Battery)"
        ></v-checkbox>
        <v-checkbox
          class="my-checkbox"
          label="Dưới 3000 mah"
          color="red"
          v-model="checkedValueBattery"
          :value="batteryValue.LessThanThree"
          hide-details
          @change="onCheckedItem(filterType.Battery)"
        ></v-checkbox>
        <v-checkbox
          class="my-checkbox"
          label="Từ 3000-5000 mah"
          color="red"
          v-model="checkedValueBattery"
          :value="batteryValue.ThreeToFive"
          hide-details
          @change="onCheckedItem(filterType.Battery)"
        ></v-checkbox>
        <v-checkbox
          class="my-checkbox"
          label="Trên 5000 mah"
          color="red"
          v-model="checkedValueBattery"
          :value="batteryValue.OverFive"
          hide-details
          @change="onCheckedItem(filterType.Battery)"
        ></v-checkbox>
      </div>
    </div>
    
  </div>
</template>

<script>
import { mapActions, mapState, mapMutations } from "vuex";
export default {
  name: "FilterProduct",
  data() {
    return {
      ex4: true,
      isCheckAllManufacturer: true,
      filter: ["", "", "", ""],
      prices: { All: true },
      batteries: { All: true },
      monitors: { All: true },
      priceNumber: 0,
      checkedValueManufacturer: [],
      checkedValuePrice: [],
      checkedValueMonitor: [],
      checkedValueBattery: [],
      priceValue: this.$enumeration.FilterPrice,
      monitorValue: this.$enumeration.FilterMonitor,
      batteryValue: this.$enumeration.FilterBattery,
      filterType: this.$enumeration.Filter,
    };
  },
  created() {
    const me = this;
    me.getManufacturers();
  },
  computed: {
    ...mapState("category", ["categories"]),
    ...mapState("manufacturer", ["manufacturers"]),
    //hiển thị bộ lọc theo hãng nếu là là phụ kiện
    showManufacturer() {
      const me = this;
      return (me.$route.query.accessory ||
        me.$route.params.category === "phu-kien" ||
        me.$route.fullPath==="/search"
        )
        ? true
        : false;
    },
    isSearch(){
      const me = this;
      return (
        me.$route.fullPath==="/search"
        )
        ? true
        : false;
    },
    isAccessory(){
      const me = this;
      return (
        me.$route.query.accessory ||
        me.$route.params.category === "phu-kien"
        )
        ? true
        : false;
    },
    /**
     * check All hãng săn xuất
     */
    isCheckAllManufacturerclone() {
      return this.checkedValueManufacturer.length == 0;
    },
    /**
     * check All giá
     */
    isCheckAllPrice() {
      return this.checkedValuePrice.length == 0;
    },
    /**
     * check All màn hình
     */
    isCheckAllMonitor() {
      return this.checkedValueMonitor.length == 0;
    },
    /**
     * check All pin
     */
    isCheckAllBattery() {
      return this.checkedValueBattery.length == 0;
    },
  },
  methods: {
    ...mapActions("manufacturer", ["getManufacturers"]),
    ...mapActions("product", ["getProductsByCategory"]),
    ...mapMutations("product", ["setFilter"]),
    ...mapMutations("manufacturer", ["setManufacturers"]),

    checkAll(filterType) {
      const me = this;
      switch (filterType) {
        case me.filterType.Manufacturer: {
          me.checkedValueManufacturer = [];
          me.filter[0] = "";
          break;
        }
        case me.filterType.Price: {
          me.checkedValuePrice = [];
          me.filter[1] = "";
          break;
        }
        case me.filterType.Battery: {
          me.checkedValueBattery = [];
          me.filter[2] = "";
          break;
        }
        case me.filterType.Monitor: {
          me.checkedValueMonitor = [];
          me.filter[3] = "";
          break;
        }
      }
      me.getProducts();
    },

    async onCheckedItem(filterType) {
      // let payload = this.checkedValueManufacturer
      // if(this.checkedValueManufacturer.length === this.manufacturers.length){
      //   payload = null
      // }
      const me = this;
      switch (filterType) {
        case me.filterType.Manufacturer: {
          const manufacturer = me.setManufacturerFilter();
          me.filter[0] = manufacturer;
          break;
        }
        case me.filterType.Price: {
          const price = me.setPriceFilter();
          me.filter[1] = price;
          break;
        }
        case me.filterType.Battery: {
          const battery = me.setBatteryFilter();
          me.filter[2] = battery;
          break;
        }
        case me.filterType.Monitor: {
          const monitor = me.setMonitorFilter();
          me.filter[3] = monitor;
          break;
        }
      }

      me.setFilter(me.filter);
      me.getProducts();
      // localStorage.setItem('FILTER',me.filter)
      // const params = {
      //   categoryID: me.$route.query.categoryID,
      //   filter:me.filter,
      //   pageSize: 1,
      //   pageIndex: 1,
      //   sortBy: 0,
      // };
      // await me.getProductsByCategory(params);
    },
    async getProducts() {
      const me = this;
      const queryText = localStorage.getItem("QUERYTEXT");

      const params = {
        categoryID: me.$route.query.categoryID
          ? me.$route.query.categoryID
          : "",
        queryText: queryText,
        filter: me.filter !== null ? me.filter : [],
        pageSize: 10,
        pageIndex: 1,
        sortBy: 0,
      };
      await me.getProductsByCategory(params);
    },
    setManufacturerFilter() {
      const me = this;
      const manufacturers = me.checkedValueManufacturer.join();
      return manufacturers;
      //lấy ra nhwuxng hãng được chọn
      // const manufacturer = me.manufacturers.filter((item) => {
      //   return item.IsCheck !== null && item.IsCheck != false;
      // });
      // //nếu tất cả các hãng được chọn
      // if (manufacturer.length === me.manufacturers.length) {
      //   me.isCheckAllManufacturer = true;
      //   manufacturers = me.manufacturers.map((item) => {
      //     item.IsCheck = false;
      //     return item;
      //   });
      //   me.setManufacturers(manufacturers);
      //   manufacturers = "";
      // } else {
      //   manufacturers = manufacturer
      //     .map((item) => {
      //       return item.ManufacturerID;
      //     })
      //     .join(",");
      //     console.log(manufacturers);
      //   me.isCheckAllManufacturer = false;
      // }
      // if(manufacturers===""){
      //   me.isCheckAllManufacturer = true;
      // }
      // return manufacturers;
    },
    /**
     * set giá trị filter giá
     */
    setPriceFilter() {
      const me = this;
      const prices = me.checkedValuePrice.join();
      return prices;
      // let price = "";
      // let priceNumber = 0;
      // //price
      // if (me.prices.All === true) {
      //   price = "";
      // }
      // if (me.prices.Two === true) {
      //   price += me.$enumeration.FilterPrice.LessThanTwo.toString() + ",";
      //   priceNumber++;
      // }
      // if (me.prices.Four === true) {
      //   price += me.$enumeration.FilterPrice.TwoToFour.toString() + ",";
      //   priceNumber++;
      // }
      // if (me.prices.Seven === true) {
      //   price += me.$enumeration.FilterPrice.FourToSeven.toString() + ",";
      //   priceNumber++;
      // }
      // if (me.prices.Fifteen === true) {
      //   price += me.$enumeration.FilterPrice.SevenToFifteen.toString() + ",";
      //   priceNumber++;
      // }
      // if (me.prices.OverFifteen === true) {
      //   price += me.$enumeration.FilterPrice.OverFifteen.toString() + ",";
      //   priceNumber++;
      // }
      // //nếu số lượng là tất cả thì giá trị rỗng là chọn tất cả
      // if (priceNumber === 5) {
      //   price = "";
      // }
      // //nếu khác rỗng thì set All=false và cắt dấu phẩy ở cuối
      // if (price !== "") {
      //   me.prices.All = false;
      //   price = price.slice(0, -1);
      // } else {
      //   me.prices.All = true;
      //   me.prices.Two = false;
      //   me.prices.Four = false;
      //   me.prices.Seven = false;
      //   me.prices.Fifteen = false;
      //   me.prices.OverFifteen = false;
      // }
      // return price;
    },
    /**
     * set giá trị filter pin
     */
    setBatteryFilter() {
      const me = this;
      const batteries = me.checkedValueBattery.join();
      return batteries;
      // let battery = "";
      // let batteryNumber = 0;
      // //battery
      // if (me.batteries.All === true) {
      //   battery = "";
      // }
      // if (me.batteries.Three === true) {
      //   battery += me.$enumeration.FilterBattery.LessThanThree.toString() + ",";
      //   batteryNumber++;
      // }
      // if (me.batteries.Five === true) {
      //   battery += me.$enumeration.FilterBattery.ThreeToFive.toString() + ",";
      //   batteryNumber++;
      // }
      // if (me.batteries.OverFive === true) {
      //   battery += me.$enumeration.FilterBattery.OverFive.toString() + ",";
      //   batteryNumber++;
      // }
      // if (batteryNumber === 3) {
      //   battery = "";
      // } else {
      //   me.batteries.All = false;
      // }
      // if (battery !== "") {
      //   battery = battery.slice(0, -1);
      // } else {
      //   me.batteries.All = true;
      //   me.batteries.Three = false;
      //   me.batteries.Five = false;
      //   me.batteries.OverFive = false;
      // }
      // return battery;
    },
    /**
     * set giá trị filter pin
     */
    setMonitorFilter() {
      const me = this;
      const monitors = me.checkedValueMonitor.join();
      return monitors;
      // let monitor = "";
      // let monitorNumber = 0;
      // //monitor
      // if (me.monitors.All === true) {
      //   monitor = "";
      // }
      // if (me.monitors.Six === true) {
      //   monitor += me.$enumeration.FilterMonitor.LessThanSix.toString() + ",";
      //   monitorNumber++;
      // }
      // if (me.monitors.OverSix === true) {
      //   monitor += me.$enumeration.FilterMonitor.OverSix.toString() + ",";
      //   monitorNumber++;
      // }
      // if (monitorNumber === 2) {
      //   monitor = "";
      // }else{
      //   me.monitors.All = false;
      // }
      // if (monitor !== "") {
      //   monitor = monitor.slice(0, -1);
      // } else {
      //    me.monitors.All = true;
      //   me.monitors.Six = false;
      //   me.monitors.OverSix = false;
      // }
      // return monitor;
    },
  },
};
</script>

<style scoped>
</style>
<style lang="scss" scoped>
.filter-product-container {
  border-radius: 4px;
  .filter-type {
    color: #212529;
    font-weight: 600;
  }

  .property {
  }
}
::v-deep {
  .my-checkbox {
    margin-top: 4px;
    .v-label {
      font-size: 14px !important;
    }
    .v-icon.v-icon {
      font-size: 16px !important;
    }
    .v-input--selection-controls__ripple {
      height: 20px;
      width: 20px;
      left: -4px;
      top: calc(50% - 18px);
    }
  }
}
</style>
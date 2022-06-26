<template>
<div class="pie-chart-container">
<div class="chartTitle tw-w-full tw-text-center tw-text-xl">{{chartTitle}}</div>
  <Doughnut
    :chart-options="chartOptions"
    :chart-data="data"
    :chart-id="chartId"
    :dataset-id-key="datasetIdKey"
    :plugins="plugins"
    :css-classes="cssClasses"
    :styles="styles"
    :width="width"
    :height="height"
  />
</div>
</template>

<script>
import { Doughnut } from 'vue-chartjs/legacy'

import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  ArcElement,
  CategoryScale
} from 'chart.js'

ChartJS.register(Title, Tooltip, Legend, ArcElement, CategoryScale)

export default {
  name: 'DoughnutChart',
  components: {
    Doughnut
  },
  props: {
    chartId: {
      type: String,
      default: 'doughnut-chart'
    },
    datasetIdKey: {
      type: String,
      default: 'label'
    },
    width: {
      type: Number,
      default: 400
    },
    height: {
      type: Number,
      default: 400
    },
    cssClasses: {
      default: '',
      type: String
    },
    styles: {
      type: Object,
      default: () => {}
    },
    plugins: {
      type: Array,
      default: () => []
    },
    chartData: {
      type: Array,
      default: () => []
    },
    labels: {
      type: Array,
      default: () => []
    },
    chartTitle:{
      type:String,
      default:''
    }
  },
  data() {
    return {
      
      chartOptions: {
        responsive: true,
        maintainAspectRatio: false
      }
    }
  },
  computed:{
    data() {
      return{

        labels: this.labels,
        datasets: [
          {
            backgroundColor: ['#41B883', '#E46651', '#00D8FF', '#DD1B16'],
            data: this.chartData
          }
        ]
      }
      },
  }
}
</script>

<template>
<div class="bar-chart-container">
  <div class="chart-title tw-w-full tw-text-center tw-text-xl">{{chartTitle}}</div>
  <Bar
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
import { Bar } from 'vue-chartjs/legacy'

import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  BarElement,
  CategoryScale,
  LinearScale
} from 'chart.js'

ChartJS.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale)

export default {
  name: 'BarChart',
  components: {
    Bar
  },
  props: {
    chartId: {
      type: String,
      default: 'bar-chart'
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
    chartTitle:{
      type:String,
      default:''
    },
    labels:{
      type:Array,
      default:()=>{}
    },
    label:{
      type:String,
      default:''
    },
    chartData:{
      type:Array,
      default:()=>{}
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
            label: this.label,
            backgroundColor: '#f87979',
            data: this.chartData
          }
        ]
      }
      },
  }
}
</script>

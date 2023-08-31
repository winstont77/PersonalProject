<script>
import { onMounted, ref } from 'vue';
import { reactive } from 'vue';
import axios from 'axios'
import router from "../../router/router"
import Plotly from 'plotly.js-dist';

export default{
    setup() {
        onMounted(()=>{
            const data = [{
                x: [1, 2, 3, 4],
                y: [10, 11, 12, 13],
                mode: 'markers',
                type: 'scatter'
            }];

            const layout = {
                title: 'Basic Scatter Plot'
            };

            Plotly.newPlot('yourDivId', data, layout);
        })
        return {}
    },
    components: {  }
}
</script>

<template>
    <div class="bmcontent">
        <div class="bmcontainer-fluid">
            <div id="yourDivId"></div>
        </div>
    </div>
</template>

<style>

</style>
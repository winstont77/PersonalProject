<script setup>
import { ref } from 'vue'

defineProps({
  odds:null,
  team:null
})

const count = ref(0)
</script>

<template>
  <div class="betResult">
    <div class="betResult-content">
        <div class="betResult-content-table">
            <table>
                <tr>
                    <th>投注金額</th>
                    <th>賠率</th>
                    <th style="width:60%">投注隊伍</th>
                </tr>
                <tr>
                    <td>100</td>
                    <td>{{ odds }}</td>
                    <td>{{ team }}</td>
                </tr>
            </table>
        </div>
        <div class="betResult-content-surebutton">
            <div class="betResult-content-surebutton-content">
                <div class="betResult-content-surebutton-text">確認</div>
                <div class="betResult-content-surebutton-textcancel">取消</div>
            </div>
        </div>
    </div>
  </div>
</template>

<style scoped>
.betResult {
  border-top: 2px solid #367a65;
  background-color: #383838;
  position: absolute;
  z-index: 100;
  color: black;
  top: 0;
  width: 100%;
  height: 100%;
}

.betResult-content{
    /* background-image: linear-gradient(rgba(12,22,20,.1),transparent 20px),radial-gradient(122% 370px at center -220px,#009969 0,transparent 100%),linear-gradient(to right bottom,#0c1614,#084436); */
}

.betResult-content-table{
    margin-top: 30px;
    width: 100%;
}

.betResult-content-table > table{
    width: 90%;
    margin: auto;
}

.betResult-content-table  table tr th{
    width: 25%;
    text-align: center;
    color: #DDDDDD;
    background-color: #333;
    padding: 5px 0;
}

.betResult-content-table  table tr td{
    text-align: center;
    color: #DDDDDD;
    padding: 5px 0;
}


.betResult-content-surebutton{  
    margin-top: 30px;
    color: #fff;
    display: flex;
}

.betResult-content-surebutton-content{
    margin: auto;
    display: flex;
}

.betResult-content-surebutton-text{
    background-color: #367a65;
    padding: 5px 10px;
    border-radius: 3px;
    cursor: pointer;
}

.betResult-content-surebutton-textcancel{
    padding: 5px 10px;
    cursor: pointer;
    margin-left: 20px;
}
</style>

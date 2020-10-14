// eslint-disable-next-line vue/valid-template-root
<template>
  <div />
</template>>

<script>

import waves from '@/directive/waves' // waves directive
// import Pagination from '@/components/Pagination' // secondary package based on el-pagination

export default {
  name: 'Dashboard',
  // components: { Pagination },
  directives: { waves },
  data() {
    return {
      tableKey: 0,
      list: [],
      menuIsShow: true,
      tableList: [],
      total: 0,
      listLoading: true,
      listQuery: {
        pageindex: 1,
        pagesize: 20,
        siteID: null,
        divisionID: null,
        stationID: null,
        projectTitle: '',
        department: ''
      },
      departmentList: [],
      stationList: []
    }
  },
  created() {
    this.listQuery.siteID = this.$store.getters.siteID
    this.getDivision()
  },
  methods: {

  }
}
</script>

<style lang="scss" scoped>
.icon-menu{
  display: flex;
  align-items: center;
  justify-content:space-around;
  height: calc(100vh - 50px);
}
.hover{
  cursor: pointer;
}

.title{
  display:block;
  text-align:center;
  margin-bottom:-10px;
  font-size:23px;
  font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
  font-weight: bold;
}

</style>

<style>
.el-table .danger-row {
    background:#fee1e2;
  }

.el-table .success-row {
    background: #f0f9eb;
  }
</style>

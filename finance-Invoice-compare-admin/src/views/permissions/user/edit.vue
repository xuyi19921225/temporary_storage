<template>
  <div class="app-container">
    <el-form ref="form" :model="form" :rules="rules" label-width="120px" class="demo-ruleForm">
      <el-row>
        <el-col :span="8">
          <el-form-item label="Site" prop="siteID">
            <el-select id="sltSite" v-model="form.siteID" filterable clearable style="width: 100%;" disabled @change="getDivisonListBySiteID">
              <el-option v-for="item in siteList" :key="item.id" :label="item.value" :value="item.id" />
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <el-form-item label="Division" prop="divisionID">
            <el-select v-model="form.divisionID" filterable clearable style="width: 100%;" disabled>
              <el-option v-for="item in divisionList" :key="item.id" :label="item.value" :value="item.id" />
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <el-form-item label="部门" prop="department">
            <el-input v-model="form.department" />
          </el-form-item>
        </el-col>
      </el-row>

      <el-row>
        <el-col :span="8">
          <el-form-item label="工号" prop="jobNumber">
            <el-input v-model="form.jobNumber" disabled />
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <el-form-item label="NTID" prop="ntid">
            <el-input v-model="form.ntid" disabled />
          </el-form-item>
        </el-col>
      </el-row>

      <el-row>
        <el-col :span="8">
          <el-form-item label="用户名" prop="userName">
            <el-input v-model="form.userName" />
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <el-form-item label="邮箱" prop="email">
            <el-input v-model="form.email" />
          </el-form-item>
        </el-col>
      </el-row>

      <el-row>
        <el-col :span="8">
          <el-form-item label="座机" prop="specialPlane">
            <el-input v-model="form.specialPlane" />
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <el-form-item label="权限" prop="roleID">
            <el-select v-model="form.roleID" filterable clearable style="width: 100%;">
              <el-option v-for="item in roleList" :key="item.id" :label="item.roleName" :value="item.id" />
            </el-select>
          </el-form-item>
        </el-col>
      </el-row>

      <el-form-item>
        <el-button type="primary" @click="onSubmit">保存</el-button>
        <el-button @click="onCancel">退出</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
import { getSiteAreaListByParameter } from '@/api/sitearea'
import { saveUser, getUserInfoByID } from '@/api/user'
import { getRoleList } from '@/api/role'

export default {
  name: 'UserEdit',
  data() {
    var checkEmail = (rule, value, callback) => {
      const mailReg = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+/
      if (!value) {
        return callback(new Error('邮箱不能为空'))
      }
      setTimeout(() => {
        if (mailReg.test(value)) {
          callback()
        } else {
          callback(new Error('请输入正确的邮箱格式'))
        }
      }, 100)
    }
    return {
      form: {
        userName: '',
        email: '',
        specialPlane: '',
        roleID: '',
        ntid: '',
        jobNumber: '',
        department: '',
        divisionID: '',
        siteID: '',
        updatedBy: ''
      },
      id: '',
      siteList: [],
      divisionList: [],
      roleList: [],
      rules: {
        userName: [
          { required: true, message: '用户名不能为空', trigger: ['blur', 'change'] }
        ],
        email: [
          { validator: checkEmail, trigger: ['blur', 'change'] }
        ],
        roleID: [
          { required: true, message: '角色不能为空', trigger: ['blur', 'change'] }
        ],
        ntid: [
          { required: true, message: 'NTID不能为空', trigger: ['blur', 'change'] }
        ],
        jobNumber: [
          { required: true, message: '工号不能为空', trigger: ['blur', 'change'] }
        ],
        department: [
          { required: true, message: '部门不能为空', trigger: ['blur', 'change'] }
        ],
        divisionID: [
          { required: true, message: 'Division不能为空', trigger: ['blur', 'change'] }
        ],
        siteID: [
          { required: true, message: 'Site不能为空', trigger: ['blur', 'change'] }
        ]
      }
    }
  },
  created() {
    this.getSiteList()
    this.getDivisonListBySiteID(this.$route.query.siteID)
    this.getRoleList()
    this.initData()
  },
  methods: {
    async initData() {
      this.id = this.$route.query.id
      await getUserInfoByID({ id: this.id }).then(res => {
        if (res.response) {
          this.setInfo(res.response)
        }
      })
    },
    async getSiteList() {
      await getSiteAreaListByParameter({ parentID: -1 }).then(res => {
        this.siteList = res.response
      })
    },
    async getDivisonListBySiteID(siteID) {
      await getSiteAreaListByParameter({ parentID: siteID !== '' ? siteID : this.form.siteID }).then(res => {
        this.divisionList = res.response
      })
    },
    getRoleList() {
      getRoleList().then(res => {
        this.roleList = res.response
      })
    },
    saveUser() {
      this.form.updatedBy = this.$store.getters.name
      this.form.id = this.id
      saveUser(this.form).then(res => {
        if (res.success === true) {
          this.$message.success('更新成功')
          this.onCancel()
        } else {
          this.$message.error(res.message)
        }
      })
    },
    setInfo(row) {
      this.form.userName = row.userName
      this.form.email = row.email
      this.form.specialPlane = row.specialPlane
      this.form.roleID = row.roleID
      this.form.ntid = row.ntid
      this.form.jobNumber = row.jobNumber
      this.form.department = row.department
      this.form.divisionID = row.divisionID
      this.form.siteID = row.siteID
    },
    onSubmit() {
      this.$refs['form'].validate((valid) => {
        if (valid) {
          this.saveUser()
        } else {
          return false
        }
      })
    },
    onReset() {
      this.$refs['form'].resetFields()
    },
    onCancel() {
      history.back()
    }
  }
}
</script>

<style scoped>
.line{
  text-align: center;
}
</style>


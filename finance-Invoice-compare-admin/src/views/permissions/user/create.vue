<template>
<div class="app-container">
    <el-form ref="form" :model="form" :rules="rules" label-width="120px" class="demo-ruleForm">
        <el-row>
            <el-col :span="8">
                <el-form-item label="Site" prop="siteID">
                    <el-select v-model="form.siteID" filterable clearable style="width: 100%;" @change="getDivisonListBySiteID">
                        <el-option v-for="item in siteList" :key="item.id" :label="item.value" :value="item.id" />
                    </el-select>
                </el-form-item>
            </el-col>
            <el-col :span="8">
                <el-form-item label="Division" prop="divisionID">
                    <el-select v-model="form.divisionID" filterable clearable style="width: 100%;">
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
                    <el-input v-model="form.jobNumber" placeholder="请输入workday工号，按回车获取账号信息" @keyup.enter.native="getSAPUserInfoByWorkdayID" />
                </el-form-item>
            </el-col>
            <el-col :span="8">
                <el-form-item label="NTID" prop="ntid">
                    <el-input v-model="form.ntid" />
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
            <el-button type="primary" @click="onReset">重置</el-button>
            <el-button @click="onCancel">退出</el-button>
        </el-form-item>
    </el-form>
</div>
</template>

<script>
import {
    getSiteAreaListByParameter
} from '@/api/sitearea'
import {
    getSAPUserInfoByWorkdayID,
    AddUser
} from '@/api/user'
import {
    getRoleList
} from '@/api/role'

export default {
    name: 'UserCreate',
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
                createBy: ''
            },
            siteList: [],
            divisionList: [],
            roleList: [],
            rules: {
                userName: [{
                    required: true,
                    message: '用户名不能为空',
                    trigger: ['blur', 'change']
                }],
                email: [{
                    validator: checkEmail,
                    trigger: ['blur', 'change']
                }],
                roleID: [{
                    required: true,
                    message: '角色不能为空',
                    trigger: ['blur', 'change']
                }],
                ntid: [{
                    required: true,
                    message: 'NTID不能为空',
                    trigger: ['blur', 'change']
                }],
                jobNumber: [{
                    required: true,
                    message: '工号不能为空',
                    trigger: ['blur', 'change']
                }],
                department: [{
                    required: true,
                    message: '部门不能为空',
                    trigger: ['blur', 'change']
                }],
                divisionID: [{
                    required: true,
                    message: 'Division不能为空',
                    trigger: ['blur', 'change']
                }],
                siteID: [{
                    required: true,
                    message: 'Site不能为空',
                    trigger: ['blur', 'change']
                }]
            }
        }
    },
    watch: {
        'form.jobNumber': function () {
            this.form.department = ''
            this.form.userName = ''
            this.form.email = ''
        },
        'form.siteID': function () {
            this.form.divisionID = ''
        }
    },
    created() {
        this.getSiteList()
        this.getRoleList()
    },
    methods: {
        getSiteList() {
            getSiteAreaListByParameter({
                isActive: 0,
                parentID: -1
            }).then(res => {
                this.siteList = res.response
            })
        },
        getDivisonListBySiteID() {
            getSiteAreaListByParameter({
                isActive: 0,
                parentID: this.form.siteID
            }).then(res => {
                this.divisionList = res.response
            })
        },
        getSAPUserInfoByWorkdayID() {
            getSAPUserInfoByWorkdayID({
                workdayID: this.form.jobNumber
            }).then(res => {
                if (res.response) {
                    this.setInfo(res.response)
                } else {
                    this.$message.warning('没有用户信息')
                }
            })
        },
        getRoleList() {
            getRoleList().then(res => {
                this.roleList = res.response
            })
        },
        createUser() {
            this.form.createBy = this.$store.getters.name
            AddUser(this.form).then(res => {
                if (res.success === true) {
                    this.$message.success('创建成功')
                    this.onReset()
                } else {
                    this.$message.error(res.message)
                }
            })
        },
        setInfo(row) {
            this.form.email = row.emailAddress
            this.form.department = row.psaName
            this.form.userName = row.nameCN
        },
        onSubmit() {
            this.$refs['form'].validate((valid) => {
                if (valid) {
                    this.createUser()
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
.line {
    text-align: center;
}
</style>

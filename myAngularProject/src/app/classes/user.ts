export class User {

    constructor(public userCode: number = 0,
        public firstName?: string,
        public lastName?: string,
        public phone?: string,
        public email?: string,
        public password?: string,
        public firstAidCertificate: boolean=false) { }
}
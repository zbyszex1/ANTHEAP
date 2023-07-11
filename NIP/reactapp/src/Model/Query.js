class Query {
  constructor() {
    this.RequestedDateTime = null;
    this.RequestedId = null;
    this.RequestedNip = null;
    this.Code = null;
    this.Message = null;
  }
  Apply(key, value) {
    switch (key) {
      case 'requestedDateTime':
        this.RequestedDateTime = value;
        break;
      case 'requestedId':
        this.RequestedId = value;
        break;
      case 'requestedNip':
        this.RequestedNip = value;
        break;
      case 'code':
        this.Code = value;
        break;
      case 'message':
        this.Message = value;
        break;
      case 'id':
        this.Id = value;
        break;
      default:
        break;
    }
  }

}

export default Query;


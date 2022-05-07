import React, { Component } from 'react';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
      this.state = { bankstatement: [], incomespending: "Income = 0 | Spending = 0",  loading: true };
  }

  componentDidMount() {
    this.populateData();
  }

  static renderTable(statements) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Transaction Date</th>
            <th>Description</th>
            <th>Amount (GHS)</th>
            <th>Transaction Type</th>
          </tr>
        </thead>
        <tbody>
          {statements.map((statement, i) =>
              <tr key={i}>
                  <td>{statement.transactionDate}</td>
                  <td>{statement.description}</td>
                  <td>{statement.totalAmount}</td>
                  <td>{statement.type ? "Credit" : "Debit"}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading, Internet is slow or not available...</em></p>
        : FetchData.renderTable(this.state.bankstatement);

    return (
      <div>
        <h1 id="tabelLabel">Bank Statement</h1>
            <p>
                {this.state.incomespending}
            </p>
        {contents}
      </div>
    );
  }

  async populateData() {
    const response = await fetch('bankstatement/query');
      const result = await response.json();
      if (result.state) {
          this.setState({ bankstatement: result.data, incomespending : result.message, loading: false });
      }
  }
}

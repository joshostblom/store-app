import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from '../components/NavMenu/NavMenu';
import { DisplayProductBoxes } from '../components/Product/ProductBox/ProductBox';

export class Layout extends Component {
  static displayName = Layout.name;

  render() {
    return (
       <div>
        <NavMenu />
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}

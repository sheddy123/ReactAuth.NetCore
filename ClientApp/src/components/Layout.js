import React from 'react';
import { Container } from 'reactstrap';
import NavMenu from './NavMenu/NavMenu';

const Layout = (props) => {
  return (
    <div>
      <NavMenu name={props.name} setName={props.setName} />
      <Container>{props.children}</Container>
    </div>
  );
};

export default Layout;

<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/CommonMaster.master" CodeFile="MakeOffer.aspx.cs" Inherits="Pages_MakeOffer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>

    <asp:Content ID="offerContent1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <script type="text/javascript" src="../Assets/scripts/datatables.js"></script>
    <script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>
    <script language="JavaScript" type="text/javascript">
        $(document).ready(function () {
           $('.ddlpoints').change(function () {
                debugger;
                var ddltext = $(this).children("option:selected").text();
                $(this).closest('tr').find('.lblpoint').text(ddltext);
           });
            
            $('body').on('click', '.btnShowcostannexure', function () {
                try {
                    debugger;
                    if ($('.lbl_itemccost').text() == '') {
                        SuccessMessage('Error', 'No records found');
                        return false;
                    }
                    $('#previewmodule').modal({
                        backdrop: 'static'
                    });
                    var gvcontnt = '';
                    var $poupcontnt1 = $($('#popuptbody').html());
                    $(this).closest('.card-body').find('table:first').find('tr').each(function () {
                        debugger;
                        if ($(this).find('td').length > 0) {
                            $poupcontnt = $poupcontnt1;
                            $poupcontnt.find('#popupsno').text($(this).find('.gvsno').text());
                            $poupcontnt.find('#popupItemname').text($(this).find('.gvItemname').text());
                            $poupcontnt.find('#popupDrawingnumber').text('AS PER ATTACHED SKETCh');
                            $poupcontnt.find('#popupSize').text('');
                            $poupcontnt.find('#popupcost').text($(this).find('.gvItemcost').text());
                            $poupcontnt.find('#popupqty').text($(this).find('.gvqty').text());
                            gvcontnt += '<tr class="rgRow" id="popuptr" style="width:180px;text-align:center;">' + $poupcontnt.html() + '</tr>';
                        }
                    });
                    $('#popuptbody').html(gvcontnt);
                    $('.popupcolumnhead').text('Annexure 2');
                    $('.popuptitle').text('Annexure 2 Preview');
                    return false;
                }
                catch (er) { }

            });
            $('body').on('click', '.btnShowpopup', function () {
               debugger;
                try {
                    $('#modelAddModule').modal({
                        backdrop: 'static'
                    });
                    var gvcontnt = '';
                    $(this).closest('.card-body').find('table').find('tr').each(function () {
                        if ($(this).find('td').length > 0 && $(this).find('[type="checkbox"]').is(':checked') == true) {
                            gvcontnt += '<h4>' + $(this).find('.lblheader').text() + '</h4>';
                            gvcontnt += '<p style="margin-top:25px !important;margin-bottom:25px !important;">' + $(this).find('.lblpoint').text() + '</p>';
                        }
                    });
                    $('.popupcontent').html(gvcontnt);
                    var anxrno = $(this).closest('.card-body').find('.lbltitle').text();
                    $('.popupcolumnhead').text(anxrno);
                    anxrno = anxrno + " Preview";
                    $('.popuptitle').text(anxrno);
                    return false;
                } catch (er) { }
            });

        });
        //function ShowPopup() {
        //    debugger;
        //    $('#modelAddModule').modal({
        //        backdrop: 'static'
        //    });
        //    debugger;
        //    var gvcontnt = '';
        //    $('#ContentPlaceHolder1_gvAnnexure1,gvAnnexure1').find('tr').each(function () {
        //        if ($(this).find('td').length > 0 && $(this).find('[type="checkbox"]').is(':checked') == true) {
        //            gvcontnt += '<h4>' + $(this).find('.sowheadername').text() + '</h4>';
        //            gvcontnt += '<p style="margin-top:25px !important;margin-bottom:25px !important;">' + $(this).find('.sowpointname').text() + '</p>';
        //        }
        //    });
        //    $('#gvcontent,#ContentPlaceHolder1_gvcontent').html(gvcontnt);
        //    return false;
        //}
    </script>
</asp:Content>
<asp:Content ID="offerContent2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container main-container_close">
         <div class="app-main__outer">
            <div class="app-main__inner">
                <div class="app-page-title">
                    <div class="page-title-left page-title-wrapper">
                        <div class="page-title-heading">
                            <div>
                                <div class="page-title-head center-elem">
                                    <span class="d-inline-block pr-2">
                                        <%--<i class="fa fa-th-large opacity-6 yellow"></i>--%>
                                        <img src="../Assets/images/pages.png" alt="" width="16px" />
                                    </span>
                                    <h3 class="page-title-head d-inline-block">
                                        Make Offer</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Sales</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Make Offer</li>
                                                </ol>
                                     </nav>
                        <a id="help" href="" alt="" style="margin-top: 4px;">
                            <img src="../Assets/images/help.png" />
                        </a>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <asp:UpdatePanel ID="upQuote" runat="server">
                <Triggers>
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">                      
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">   
                                     <div class="col-sm-12 p-t-10 p-l-0 p-r-0">
                                            <table align="center" id="tbl_customerDetails" class="tr_class table">
	<tbody>
		<tr class="eqheading">
			<td>
				<div class="offerFormHeading1">Customer Details</div>							 
			</td>
		</tr>
		<tr>
			<td align="left">
				<label id="lab_Customername" class="eqdetailslabel">Customer Name</label>
			</td>
			<td>
				<asp:DropDownList id="ddlcustomers" CssClass="form-control" runat="server" OnChange="showLoader();" OnSelectedIndexChanged="ddlcustomers_IndexChanged" AutoPostBack="true" ToolTip="Select Cust Name">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                </asp:DropDownList>
			</td>
			<td align="left">
				<span id="lab_Enquirynumber" class="eqdetailslabel">Enquiry Number</span>
				<span id="Label290" class="requiredfield">*</span>
			</td>
			<td>
				<asp:DropDownList id="ddlenquiries" CssClass="form-control" runat="server" OnChange="showLoader();" OnSelectedIndexChanged="ddlenquiries_IndexChanged" AutoPostBack="true" ToolTip="Select Enquiry Number">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                </asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td align="left">
				<span id="lab_FaxNo" class="eqdetailslabel"> Fax No</span>
			</td>
			<td align="left">
				<asp:label class="offertextbox" id="txt_FaxNo" runat="server"></asp:label>
                </td>
				<td align="left">
					<span id="lab_CustomerphoneNo" class="eqdetailslabel">Customer Phone No</span>
				</td>
				<td align="left">
					<asp:label class="offertextbox" id="txt_CustomerphoneNo" runat="server"></asp:label>
					</td>
				</tr>
				<tr>
					<td align="left">
						<span id="lab_CustomerEmail" class="eqdetailslabel"> Email</span>
					</td>
					<td align="left">
                        <asp:label class="offertextbox" id="txt_CustomerEmail" runat="server"></asp:label>
						</td>
						<td align="left">
							<span id="lab_ContactPerson" class="eqdetailslabel"> Contact Person</span>
						</td>
						<td align="left">
							<asp:label class="offertextbox" id="txt_ContactPerson" runat="server"></asp:label>	
                        </td>
						</tr>
						<tr>
							<td align="left">
								<span id="lbl_CustomerMobile" class="eqdetailslabel">Customer Mobile No</span>
							</td>
							<td align="left">
                                <asp:label class="offertextbox" id="txt_CustomerMobile" runat="server"></asp:label>	
								</td>
								<td align="left">
									<span id="lbl_ContactPersonEmail" class="eqdetailslabel">ContactPerson Email</span>
								</td>
								<td align="left">
                                    <asp:label class="offertextbox" id="txt_ContactPersonEmail" runat="server"></asp:label>	
									</td>
								</tr>
								<tr class="eqheading">
									<td>
										<div class="offerFormHeading1">Marketing Engineer</div>													
									</td>
								</tr>
								<tr>
									<td align="left">
										<span id="lbl_MarktEngg" class="eqdetailslabel">Markt Engg</span>
										<span id="Label1" class="requiredfield">*</span>
									</td>
									<td>
                                        <asp:label class="offertextbox" id="txt_MarktEngg" runat="server"></asp:label>											
									</td>
									<td align="left">
										<span id="lbl_MarktDesignation" class="eqdetailslabel"> Designation</span>
									</td>
									<td align="left">
                                        <asp:label class="offertextbox" id="txt_Marktdesignation" runat="server"></asp:label>
										</td>
									</tr>
									<tr>
										<td align="left">
											<span id="lab_MarktMobileNo" class="eqdetailslabel"> Mobile No</span>
										</td>
										<td align="left">
                                            <asp:label class="offertextbox" id="txt_MarktMobileNo" runat="server"></asp:label>											
											</td>
											<td align="left">
												<span id="lbl_Marktemail" class="eqdetailslabel"> E-Mail</span>
											</td>
											<td align="left">
                                                <asp:label class="offertextbox" id="txt_Marktemail" runat="server"></asp:label>																							
												</td>
											</tr>
											<tr>
												<td align="left">
													<span id="lbl_MarktOfficePhoneNo" class="eqdetailslabel"> Office Phone No</span>
												</td>
												<td align="left">
                                                    <asp:label class="offertextbox" id="txt_MarktOfficePhoneNo" runat="server"></asp:label>
													</td>
												</tr>
												<tr class="eqheading">
													<td>
														<div class="offerFormHeading1">Head</div>				
													</td>
												</tr>
												<tr>
													<td align="left">
														<span id="lbl_HeadName" class="eqdetailslabel">Name</span>
														<span id="Label3" class="requiredfield">*</span>
													</td>
													<td>
                                                        <asp:label class="offertextbox" id="txt_HeadName" runat="server"></asp:label>														
													</td>
													<td align="left">
														<span id="lbl_HDesignation" class="eqdetailslabel"> Designation</span>
													</td>
													<td align="left">
                                                        <asp:label class="offertextbox" id="txt_HDesignation" runat="server"></asp:label>														
														</td>
													</tr>
													<tr>
														<td align="left">
															<span id="lbl_HMobileNo" class="eqdetailslabel"> Mobile No</span>
														</td>
														<td align="left">
                                                            <asp:label class="offertextbox" id="txt_HMobileNo" runat="server"></asp:label>
															</td>
															<td align="left">
																<span id="lbl_HEmail" class="eqdetailslabel"> E-Mail</span>
															</td>
															<td align="left">
                                                                <asp:label class="offertextbox" id="txt_HEmail" runat="server"></asp:label>
																</td>
															</tr>
															<tr class="eqheading">
																<td>
																	<div class="offerFormHeading1">Sub Head</div>
																</td>
															</tr>
															<tr>
																<td align="left">
																	<span id="lbl_subheadname" class="eqdetailslabel">Name</span>
																	<span id="Label4" class="requiredfield">*</span>
																</td>
																<td>
                                                                    <asp:label class="offertextbox" id="txt_SubHeadName" runat="server"></asp:label>
																	</td>
																<td align="left">
																	<span id="lbl_SHdesignation" class="eqdetailslabel"> Designation</span>
																</td>
																<td align="left">
                                                                    <asp:label class="offertextbox" id="txt_SHdesignation" runat="server"></asp:label>																	
																	</td>
																</tr>
																<tr>
																	<td align="left">
																		<span id="lbl_SHMobileNo" class="eqdetailslabel"> Mobile No</span>
																	</td>
																	<td align="left">
                                                                        <asp:label class="offertextbox" id="txt_SHMobileNo" runat="server"></asp:label>
																		</td>
																		<td align="left">
																			<span id="lbl_SHEmail" class="eqdetailslabel"> E-Mail</span>
																		</td>
																		<td align="left">
                                                                            <asp:label class="offertextbox" id="txt_SHEmail" runat="server"></asp:label>
																			</td>
																		</tr>
																	</tbody>
																</table>
                                        </div>
                                         <div class="ip-div text-center">                                      
                                       </div>
                                    </div>
                                     <div class="card-body">   
                                        <div class="row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                    <asp:Label ID="Label5" runat="server" class="lbltitle" Text="Front Page"></asp:Label></p>
                                            </div>                                            
                                        </div>
                                        <div class="col-sm-12 p-t-10 p-l-0 p-r-0">
                                            <table align="center" id="tbl_frontpage" class="tr_class table">
                                                <tbody>                                                  
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lbl_subitemname" class="eqdetailslabel">Subject Item</span>
                                                            <span class="requiredfield">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control"  ID="Label6" runat="server" />
                                                        </td>
                                                        <td align="left">
                                                            <span id="lbl_projectname" class="eqdetailslabel">Project Name</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox CssClass="form-control"  ID="Label7" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lbl_Reference" class="eqdetailslabel">Reference</span>
                                                            <span id="lbl4" class="requiredfield">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control"  ID="Label8" runat="server" />
                                                        </td>
                                                        <td align="left">
                                                            <span id="lbl_frntpage" class="eqdetailslabel">Front Page</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlfornt" CssClass="form-control" runat="server" ToolTip="Front Page">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Dear Sir / Madam" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Dear Sir" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="Dear Madam" Value="3"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                         <div class="ip-div text-center">
                                     <asp:Button runat="server" CssClass="btn btn-cons btn-success btnShowcostannexure" Text="Preview"/>
                                       </div>
                                    </div>
                                    <div class="card-body">   
                                        <div class="row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                    <asp:Label ID="lbltitle" runat="server" class="lbltitle" Text="Annexure 1"></asp:Label></p>
                                            </div>                                            
                                        </div>
                                        <div class="col-sm-12 p-t-10 p-l-0 p-r-0">
                                            <asp:GridView ID="gvAnnexure1" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                CssClass="table table-hover table-bordered medium" OnRowDataBound="gvAnnexure1_RowDataBound"
                                                HeaderStyle-HorizontalAlign="Center" EmptyDataText="No Records Found" DataKeyNames="SOWID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'
                                                                Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Scope to Display" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox Checked="true"  ID="chksow" runat="server"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Scope Header" HeaderText="Scope Header" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSOWHeader" class="lblheader" runat="server" Text='<%# Eval("HeaderName")%>' Style="text-align: center"></asp:Label>
                                                            <asp:Label ID="lblSOWID" runat="server" Text='<%# Eval("SOWID")%>' Style="display: none"></asp:Label>                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Scope Point" HeaderText="Scope Point" ItemStyle-Width="35%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsowPoint" class="lblpoint" runat="server" Text='<%# Eval("Point")%>' Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Select Scope Point" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlsowpoints" runat="server" CssClass="form-control ddlpoints">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                   
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnAnnexure1SOWID" runat="server" Value="0" />
                                        </div>
                                         <div class="ip-div text-center">
                                     <asp:Button runat="server" CssClass="btn btn-cons btn-success btnShowpopup" Text="Preview"/>
                                       </div>
                                    </div>
                                     <div class="card-body">   
                                        <div class="row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                    <asp:Label ID="Label2" runat="server" class="lbltitle" Text="Annexure 2"></asp:Label></p>
                                            </div>                                            
                                        </div>
                                        <div class="col-sm-12 p-t-10 p-l-0 p-r-0">
                                            <asp:GridView ID="gvAnnexure2" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                CssClass="table table-hover table-bordered medium" 
                                                HeaderStyle-HorizontalAlign="Center" EmptyDataText="No Records Found" DataKeyNames="EnquiryID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" class="gvsno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'
                                                                Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Item Name" HeaderText="Item Name" ItemStyle-Width="40%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPoint" runat="server" Text='<%# Eval("Itemname")%>' Style="text-align: center"></asp:Label>                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Quantity" HeaderText="Quantity" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTACHeader"  runat="server" Text='<%# Eval("Qty")%>' Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Drawing Name" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblfilename" runat="server" Text='<%# Eval("FileName")%>' Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Final Cost" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:label  class="lbl_itemccost gvItemcost" id="lbl_itemccost"  Text='<%# Eval("FinalCost")%>' runat="server" style="margin:0px"></asp:label>
                                                        </ItemTemplate>                                                         
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>                                            
                                        </div>
                                         <div class="ip-div text-center">
                                     <asp:Button runat="server" CssClass="btn btn-cons btn-success btnShowcostannexure" Text="Preview"/>
                                       </div>
                                    </div>
                                    <div class="card-body">   
                                        <div class="row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                    <asp:Label ID="lblanxr3" class="lbltitle" runat="server" Text="Annexure 3"></asp:Label></p>
                                            </div>                                            
                                        </div>
                                        <div class="col-sm-12 p-t-10 p-l-0 p-r-0">
                                            <asp:GridView ID="gvAnnexure3" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                CssClass="table table-hover table-bordered medium" OnRowDataBound="gvAnnexure3_RowDataBound"
                                                HeaderStyle-HorizontalAlign="Center" EmptyDataText="No Records Found" DataKeyNames="TACID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'
                                                                Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TAC to Display" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox Checked="true"  ID="chktac" runat="server"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="TAC Header" HeaderText="TAC Header" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTACHeader" class="lblheader" runat="server" Text='<%# Eval("HeaderName")%>' Style="text-align: center"></asp:Label>
                                                            <asp:Label ID="lblTACID" runat="server" Text='<%# Eval("TACID")%>' Style="display: none"></asp:Label>                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="TAC Point" HeaderText="TAC Point" ItemStyle-Width="35%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTACPoint" class="lblpoint" runat="server" Text='<%# Eval("Point")%>' Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Select TAC Point" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddltacpoints" runat="server" CssClass="form-control ddlpoints">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                   
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnAnnexure3TACID" runat="server" Value="0" />
                                        </div>
                                        <div class="ip-div text-center">
                                     <asp:Button runat="server" CssClass="btn btn-cons btn-success btnShowpopup" Text="Preview"/>
                                       </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
      <div class="modal" id="modelAddModule">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                        ×</button>
                    <h4 class="modal-title bold popuptitle" style="color: #800b4f;" id="tournamentNameTeam"></h4>
                </div>
                <div class="">
                    <div class="inner-container">
                        <div class="col-sm-12" style="padding-top: 30px;">
                           <img src="http://183.82.33.21/lserpmirror/images/topstrrip.jpg" alt="" width="100%">
                        </div>
                        <div class="col-sm-12" style="padding-top: 10px;" id="trLevel4" runat="server">
                            <div class="col-sm-4 popupcolumnhead" style="font-weight: 600;padding-top: 10px;border: 1px solid;"></div>
                            <div class="col-sm-8" style="font-weight: 600;padding-top: 10px;border: 1px solid;">
                                Material details and date
                            </div>                            
                        </div>
                         <div id="gvcontent" class="col-sm-12 popupcontent" style="padding-top: 10px;padding-left: 20px;" runat="server">                            
                        </div>
                         <div class="col-sm-12" style="padding-top: 10px;" id="Div1" runat="server">
                            <div class="col-sm-6" style="font-weight: 600;padding-top: 10px;border: 1px solid;">
                                Footer will come here</div>
                            <div class="col-sm-6" style="font-weight: 600;padding-top: 10px;border: 1px solid;">
                                Footer will come here
                            </div>                            
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>
    <div class="modal" id="previewmodule">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                        ×</button>
                    <h4 class="modal-title bold popuptitle" style="color: #800b4f;" id="tournamentNameTeam"></h4>
                </div>
                <div class="">
                    <div class="inner-container">
                        <div class="col-sm-12" style="padding-top: 30px;">
                           <img src="http://183.82.33.21/lserpmirror/images/topstrrip.jpg" alt="" width="100%">
                        </div>
                        <div class="col-sm-12" style="padding-top: 10px;" id="Div2" runat="server">
                            <div class="col-sm-4 popupcolumnhead" style="font-weight: 600;padding-top: 10px;border: 1px solid;"></div>
                            <div class="col-sm-8" style="font-weight: 600;padding-top: 10px;border: 1px solid;">
                                Material details and date
                            </div>                            
                        </div>
                         <div id="Div3" class="col-sm-12 previewpopupcontent" style="padding-top: 10px;padding-left: 20px;" runat="server">
                             <h4>Price Schedule:</h4>
                             <p style="margin-top: 25px !important; margin-bottom: 25px !important;">Given  below is the Schedule of prices for LONESTAR Make Metal Expansion Joints / Bellows as per our Drawings</p>

                             <table cellspacing="0" class="rgMasterTable" border="0" id="radgv_makeoffer_ctl00" style="width: 100%; table-layout: auto; empty-cells: show; border: 1px solid;">
                                 <colgroup>
                                     <col style="width: 180px">
                                     <col style="width: 180px">
                                     <col style="width: 180px">
                                     <col style="width: 180px">
                                     <col style="width: 180px">
                                     <col style="width: 180px">
                                 </colgroup>
                                 <thead style="background-color: whitesmoke; border: 1px solid;">
                                     <tr>
                                         <th scope="col" class="rgHeader" style="text-align: center;">SI.No</th>
                                         <th scope="col" class="rgHeader" style="text-align: center;">Item Name</th>
                                         <th scope="col" class="rgHeader" style="text-align: center;">Drawing Number</th>
                                         <th scope="col" class="rgHeader" style="text-align: center;">Size</th>
                                         <th scope="col" class="rgHeader" style="text-align: center;">Total  Price</th>
                                         <th scope="col" class="rgHeader" style="text-align: center;">Quantity</th>
                                     </tr>
                                 </thead>
                                 <tbody id="popuptbody">
                                     <tr class="rgRow" id="popuptr" style="width: 180px; text-align: center;">
                                         <td>
                                             <span id="popupsno">1</span>
                                         </td>
                                         <td>
                                             <span id="popupItemname"></span>
                                         </td>
                                         <td>
                                             <span id="popupDrawingnumber"></span>
                                         </td>
                                         <td>
                                             <span id="popupSize">1000 ID</span>
                                         </td>
                                         <td>
                                             <span id="popupcost"></span>
                                         </td>
                                         <td>
                                             <span id="popupqty"></span>
                                         </td>
                                     </tr>
                                 </tbody>
                             </table>
                        </div>
                         <div class="col-sm-12" style="padding-top: 100px;" id="Div4" runat="server">
                            <div class="col-sm-6" style="font-weight: 600;padding-top: 10px;border: 1px solid;">
                                Footer will come here</div>
                            <div class="col-sm-6" style="font-weight: 600;padding-top: 10px;border: 1px solid;">
                                Footer will come here
                            </div>                            
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>
</asp:Content>

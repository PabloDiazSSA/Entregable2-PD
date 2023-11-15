using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Entregable2_PD.Tools.Helpers
{
	/// <summary>
	/// 
	/// </summary>
    public class FileClass
    {
		/// <summary>
		/// 
		/// </summary>
        public string? FileName { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public string? ContentType { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public byte[]? File { get; set; }
    }
	/// <summary>
	/// 
	/// </summary>
    public class MailClass
    {
		/// <summary>
		/// 
		/// </summary>
        public string[]? to { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public string[]? cc { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public string? subject { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public string? body { get; set; }
		/// <summary>
		/// fhfgh
		/// </summary>
        public List<FileClass>? Files { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="title"></param>
		/// <param name="bodyText"></param>
		/// <param name="link"></param>
		/// <param name="linkText"></param>
		/// <param name="noRespond"></param>
		/// <param name="copyRight"></param>
		/// <param name="urlLogo"></param>
		/// <returns></returns>
        public static string MailStringHtml(string title, string bodyText, string link, string linkText, string noRespond, string copyRight, string urlLogo = "https://www.ssamexico.com/assets/image/logo/logo.png")
        {
            string strMail = @"<!DOCTYPE html>
								<html lang='en' xmlns='http://www.w3.org/1999/xhtml' xmlns:o='urn:schemas-microsoft-com:office:office'>
								<head>
									<meta charset='UTF-8'>
									<meta name='viewport' content='width=device-width,initial-scale=1'>
									<meta name='x-apple-disable-message-reformatting'>
									<title></title>
									<!--[if mso]>
									<noscript>
										<xml>
											<o:OfficeDocumentSettings>
												<o:PixelsPerInch>96</o:PixelsPerInch>
											</o:OfficeDocumentSettings>
										</xml>
									</noscript>
									<![endif]-->
									<style>
										table, td, div, h1, p {font-family: Arial, sans-serif;}
									</style>
								</head>
								<body style='margin:0;padding:0;'>
									<table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;background:#ffffff;'>
										<tr>
											<td align='center' style='padding:0;'>
												<table role='presentation' style='width:602px;border-collapse:collapse;border:1px solid #cccccc;border-spacing:0;text-align:left;'>
													<tr>
														<td style='padding:20px 0 20px 25%;background:#58a120; color: #ffffff;'>
															<span style='font-size:42px;margin:0 0 20px 0;font-family:Arial Narrow,sans-serif;font-weight: bold;'>SSA México</span>
															<span style='font-size:12px;font-family:Arial,sans-serif;font-weight: bold;'>MR</span>
															<p style='text-align: start; font-size:20px;margin:0 0 0 0;font-family:Arial Narrow,sans-serif;'>Grupo Carrix</p>
														</td>
													</tr>
													<tr>
														<td style='padding:36px 30px 42px 30px;'>
															<table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;'>
																<tr>
																	<td style='padding:0 0 36px 0;color:#153643;'>
																		<h1 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'>" + title + @"</h1>
																		<p style='margin:0 0 12px 0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'>" + bodyText + @"</p>
																		<p style='margin:0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'><a href='" + link + @"' style='color:#ee4c50;text-decoration:underline;'>" + linkText + @"</a></p>
																	</td>
																</tr>
																<tr>
																	<td align='center'>
																	</td>
																</tr>
																<tr>
																	<td style='padding:0;'>
																		<table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;'>
																			<tr>
																				<td style='width:260px;padding:0;vertical-align:top;color:#153643;'>
																					<!--Columna izquierda-->
																				</td>
																				<td style='width:20px;padding:0;font-size:0;line-height:0;'>&nbsp;</td>
																				<td style='width:260px;padding:0;vertical-align:top;color:#153643;'>
																					<!--Columna derecha-->
																				</td>
																			</tr>
																		</table>
																	</td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td style='padding:30px;background:#58a120 ;'>
															<table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;font-size:9px;font-family:Arial,sans-serif;'>
																<tr>
																	<td style='padding:0;width:90%;' align='left'>
																		<p style='margin:0;font-size:14px;line-height:16px;font-family:Arial,sans-serif;color:#ffffff;'>
																			" + noRespond + @"<br/>
																			&reg; " + copyRight + @"<br/>
																		</p>
																	</td>
																	<td style='padding:0;width:10%;' align='right'>
																		
																	</td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</body>
								</html>";

            return strMail;

        }
    }
}

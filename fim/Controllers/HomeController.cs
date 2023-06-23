using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fim.Models;
using System.IO;
using System.Drawing;
using PagedList;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace fim.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {

        public ActionResult ListaReport()
        {
            using (standEntities bd = new standEntities())
            {
                Document doc = new Document(PageSize.A4.Rotate(), 20f, 20f, 20f, 20f);
                MemoryStream memo = new MemoryStream();
                try
                {
                    PdfWriter w = PdfWriter.GetInstance(doc, memo);
                    w.CloseStream = false;
                    doc.Open();
                    PdfPTable tab = new PdfPTable(3);
                    tab.SetWidths(new float[] { 44f,200f,200f});
                    Chunk ck = new Chunk("Listagem de Marcas", FontFactory.GetFont("Arial", 22f, BaseColor.BLACK));
                    PdfPCell cell = new PdfPCell(new Phrase(ck));
                    cell.Colspan = 3;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Padding = 22f;
                    tab.AddCell(cell);
                    tab.CompleteRow();
                    cell = new PdfPCell(new Phrase("codmarca"));
                    tab.AddCell(cell);
                    cell = new PdfPCell(new Phrase("marca"));
                    tab.AddCell(cell);
                    cell = new PdfPCell(new Phrase("logotipo"));
                    tab.AddCell(cell);
                    tab.CompleteRow();
                   
                    foreach(marca m in bd.marcas)
                    {
                        cell = new PdfPCell(new Phrase(m.codmarca.ToString()));
                        tab.AddCell(cell);
                        cell = new PdfPCell(new Phrase(m.marca1));
                        tab.AddCell(cell);
                        if (m.logotipo != null)
                        {
                            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(m.logotipo);
                            cell = new PdfPCell(img);
                            cell.Padding = 12f;
                            tab.AddCell(cell);
                        }
                        else {

                            System.Drawing.Image img= System.Drawing.Image.FromFile(Server.MapPath("~/fotos/nofoto.jpg"));

                            img = img.GetThumbnailImage(40, 30, null, new IntPtr());
                            MemoryStream mfoto = new MemoryStream();
                            img.Save(mfoto, System.Drawing.Imaging.ImageFormat.Jpeg);
                            iTextSharp.text.Image img2 = iTextSharp.text.Image.GetInstance(mfoto.ToArray());
                            cell = new PdfPCell(img2);
                            cell.Padding = 12f;
                            tab.AddCell(cell);
                        }

                        tab.CompleteRow();
                        

                    }
                    doc.Add(tab);


                }
                catch (DocumentException docerror)
                {

                    throw;
                }
                catch(IOException error)
                {
                    throw;
                }
                doc.Close();

                return File(memo.ToArray(), "application/pdf");
            }
        }

        public ActionResult RelatorioII()
        {
            Document doc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 20f, 20f, 20f, 20f);
            MemoryStream memo = new MemoryStream();
           
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(doc, memo);
                writer.CloseStream = false;
                doc.Open();
                Chunk ck = new Chunk("O meu primeiro Relatório", FontFactory.GetFont("Arial", 33f, BaseColor.PINK));
                Paragraph p = new Paragraph(ck);
                p.Alignment = Element.ALIGN_CENTER;
                doc.Add(p);

                Anchor anchor = new Anchor(new Chunk("Istec", FontFactory.GetFont("Times New Roman", 12f, BaseColor.MAGENTA)));
                anchor.Reference = "http://www.google.com";
                doc.Add(anchor);

                List lst = new List(List.ORDERED, 13f);
                lst.Add(new ListItem(new Chunk("Alfa")));
                lst.Add(new ListItem(new Chunk("Bravo")));
                lst.Add(new ListItem(new Chunk("Bravo")));
                doc.Add(lst);

                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Server.MapPath("~/fotos/nofoto.jpg"));
                doc.Add(img);


                PdfContentByte cb = writer.DirectContent;
                cb.Rectangle(0, 0, 50, 50);
                cb.SetColorStroke(BaseColor.RED);
                cb.SetColorFill(BaseColor.YELLOW);
                cb.FillStroke();




                ColumnText ct = new ColumnText(cb);
                Phrase myText = new Phrase("TEST paragraph\nNewline");
                ct.SetSimpleColumn(myText, 400, 300, 500, 400, 15, Element.ALIGN_LEFT);
                ct.Go();
               
            }
            catch (DocumentException docerror)
            {

                throw;
            }
            catch(IOException ioerror)
            {
                throw;
            }
            
            
            doc.Close();
            String caminho = Server.MapPath("~/fotos/eco.pdf");
            if (System.IO.File.Exists(caminho)) System.IO.File.Delete(caminho);
             FileStream fs = new FileStream(caminho, FileMode.CreateNew, FileAccess.Write);
            memo.WriteTo(fs);
            fs.Flush();
            fs.Close();
            return File(memo.ToArray(), "application/pdf");
        }

        public ActionResult Relatorio()
        {
            Document doc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 20f, 20f, 20f, 20f);
            MemoryStream memo = new MemoryStream();
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(doc,memo);
                doc.Open();
                Chunk ck = new Chunk("O nosso Relatório \n exemplo", FontFactory.GetFont("Arial", 33f, BaseColor.PINK));
                Paragraph p = new Paragraph(ck);
                p.Alignment = Element.ALIGN_CENTER;
                doc.Add(p);

                Phrase frase = new Phrase(new Chunk("Nova Frase", FontFactory.GetFont("Times New Roman",22f,BaseColor.BLUE)));
                doc.Add(frase);

                List lst = new List(List.ORDERED, 13f);
                lst.Add(new ListItem("Alfa"));
                lst.Add(new ListItem("Bravo"));
                lst.Add(new ListItem("Charlie"));
                doc.Add(lst);

                Anchor anc = new Anchor(new Chunk("Istec", FontFactory.GetFont("Arial", 12f, BaseColor.MAGENTA)));
                anc.Reference = "http://www.google.com";
                doc.Add(anc);
                doc.Add(new Chunk("\n\n"));
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Server.MapPath("~/fotos/nofoto.jpg"));
                doc.Add(img);

                PdfContentByte cb = writer.DirectContent;

                /*cb.BeginText();
                cb.SetFontAndSize(BaseFont.CreateFont("c:\\windows\\fonts\\calibrib.ttf", BaseFont.CP1257, BaseFont.NOT_EMBEDDED), 33);
                cb.ShowText("Hello World");
                cb.EndText();
                 */

                cb.Rectangle(0, 0, 100, 100);
                cb.SetColorStroke(BaseColor.RED);
                cb.SetColorFill(BaseColor.RED);
                cb.FillStroke();

   


                ColumnText ct = new ColumnText(cb);
                Phrase myText = new Phrase("TEST paragraph\nNewline");
                ct.SetSimpleColumn(myText, 400, 300, 500, 400, 15, Element.ALIGN_LEFT);
                ct.Go();

            }
            catch (DocumentException docerror) {
                throw;
            }
            catch (IOException ioerr)
            {

                throw;
            }
            doc.Close();
            
            return File(memo.ToArray(), "application/pdf");
        }
        public ActionResult Report1()
        {
            Document doc = new Document(PageSize.A4.Rotate(), 20f, 20f, 20f, 20f);
            MemoryStream memo = new MemoryStream();
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(doc, memo);
                doc.Open();

                Phrase frase = new Phrase(new Chunk("Olá Mundo", FontFactory.GetFont("Arial", 22f, BaseColor.RED)));
                doc.Add(frase);

                List list5 = new List(List.ORDERED, 33);
                list5.Add(new ListItem("alfa"));
                list5.Add(new ListItem("Bravo"));
                list5.Add(new ListItem("Charlie"));
                list5.Add(new ListItem("Eco"));
                doc.Add(list5);

                Anchor anchor = new Anchor(new Chunk("pupilos", FontFactory.GetFont("Arial", 12f, BaseColor.BLUE)));
                anchor.Reference = "http://www.pupilos.eu";
                doc.Add(anchor);

                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(HttpContext.Server.MapPath("~/fotos/nofoto.jpg"));
                doc.Add(img);
            }
            catch (DocumentException docerro)
            {

                throw;
            }
            catch (IOException exerro)
            {
                throw;
            }

            doc.Close();

            //------------Aula 22-1 tarde---------------
            //Ao fazer click na aba do Relatorio, visualiza e guarda no caminho estipulado
            String caminho = Server.MapPath("~/fotos/bravo.pdf");
            FileStream fs = new FileStream(caminho, FileMode.CreateNew, FileAccess.Write);
            fs.Write(memo.ToArray(), 0, (int)memo.Length);
            fs.Flush();
            fs.Close();
            //---------Fim de Aula 22-1 tarde-----------

            return File(memo.ToArray(), "application/pdf");
        }

        public enum COMBUSTÍVEL
        {
            GASOLINA = 1,
            GASÓLEO = 2,
            GAZ = 3
        }

        public ActionResult DeleteModelo(int? id)
        {
            using (standEntities bd = new standEntities())
            {
                modelo morto = bd.modelos.Find(id);
                if (morto != null)
                {
                    bd.modelos.Remove(morto);
                    try
                    {
                        bd.SaveChanges();
                        return Json(new { msg = "Registo apagado com sucesso" }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception)
                    {
                        return Json(new { msg = "Não foi possível apagar o registo" }, JsonRequestBehavior.AllowGet);

                    }

                }
                return Json(new { msg = "Erro na eliminação do Registo" }, JsonRequestBehavior.AllowGet);

            }

        }


        [HttpPost]
        public ActionResult EditModelo(modelo novo, HttpPostedFileBase fich)
        {

            using (standEntities bd = new standEntities())
            {
                modelo este = bd.modelos.Find(novo.idmodelo);
                if (este != null)
                {
                    este.modelo1 = novo.modelo1;
                    este.marca = novo.marca;
                    este.potencia = novo.potencia;
                    este.combustivel = novo.combustivel;
                    if (fich != null && fich.ContentLength > 0 && fich.ContentType.Contains("image"))
                    {
                        string caminho = "~/fotosmodelos/" + este.idmodelo + System.IO.Path.GetExtension(fich.FileName);
                        este.fotomodelo = caminho;
                        fich.SaveAs(HttpContext.Server.MapPath(caminho));
                    }
                    bd.SaveChanges();
                    return RedirectToAction("ListarModelos", new { msg = "Registo Editado com sucesso" });
                }
                return RedirectToAction("ListarModelos", new { msg = "Erro na edição do Registo" });

            }

        }
        public ActionResult EditModelo(int? id)
        {


            using (standEntities bd = new standEntities())
            {
                List<marca> marcas = bd.marcas.ToList();
                ViewBag.marcas = new SelectList(marcas, "codmarca", "marca1");
                modelo este = bd.modelos.Find(id);
                if (este != null)
                {
                    return View(este);
                }
                else return RedirectToAction("ListarModelos", new { msg = "Erro registo não existe" });
            }
        }

        public ActionResult CriarModelo()
        {
            modelo novo = new modelo();
            using (standEntities bd = new standEntities())
            {
                List<marca> marcas = bd.marcas.ToList();
                ViewBag.marcas = new SelectList(marcas, "codmarca", "marca1");
            }
            return View(novo);
        }

        [HttpPost]
        public ActionResult CriarModelo(modelo novo, HttpPostedFileBase fich)
        {

            using (standEntities bd = new standEntities())
            {
                bd.modelos.Add(novo);
                bd.SaveChanges();
                if (fich != null && fich.ContentLength > 0 && fich.ContentType.Contains("image"))
                {
                    String caminho = "~/fotosmodelos/" + novo.idmodelo + System.IO.Path.GetExtension(fich.FileName);
                    fich.SaveAs(HttpContext.Server.MapPath(caminho));
                    novo.fotomodelo = caminho;
                    bd.SaveChanges();
                }
                return RedirectToAction("ListarModelos", new { msg = "Inserido com sucesso" });
            }

        }

        public ActionResult DetalheModelo(int? id)
        {
            using (standEntities bd = new standEntities())
            {
                modelo m = bd.modelos.Find(id);
                if (m != null)
                {
                    return View(m);
                }
                else
                {
                    return RedirectToAction("ListarModelos", new { msg = "Erro" });
                }
            }
        }

        public ActionResult ListarModelos(String msg, string search, string filtro, string order, int? page)
        {
            ViewBag.msg = msg;
            using (standEntities bd = new standEntities())
            {

                List<modelo> modelos = bd.modelos.ToList();
                int pagesize = 3;
                int pagina = page ?? 1;
                ViewBag.order = order;
                if (!String.IsNullOrEmpty(search))
                {
                    page = 1;
                }
                else
                {
                    search = filtro;

                }
                if (!String.IsNullOrEmpty(search))
                {
                    modelos = modelos.Where(x => x.modelo1.Contains(search)).ToList<modelo>();

                }
                ViewBag.filtro = search;

                ViewBag.idmodelo = (String.IsNullOrEmpty(order)) ? "idmodelodesc" : "";
                ViewBag.modelo = (order == "modeloasc") ? "modelodesc" : "modeloasc";
                switch (order)
                {
                    case "modeloasc":
                        modelos = modelos.OrderBy(x => x.modelo1).ToList<modelo>();

                        break;
                    case "modelodesc":
                        modelos = modelos.OrderByDescending(x => x.modelo1).ToList<modelo>();

                        break;
                    case "idmodelodesc":
                        modelos = modelos.OrderByDescending(x => x.idmodelo).ToList<modelo>();
                        break;
                    default:
                        modelos = modelos.OrderBy(x => x.idmodelo).ToList<modelo>();
                        break;
                }
                return View(modelos.ToPagedList(pagina, pagesize));

            }

        }


        public ActionResult DeleteMarca(int? id)
        {
            using (standEntities bd = new standEntities())
            {
                try
                {
                    marca m = bd.marcas.Find(id);
                    if (m != null) bd.marcas.Remove(m);
                    bd.SaveChanges();
                    return RedirectToAction("Marcas", new { msg = "Registo Apagado com sucesso" });

                }
                catch (Exception)
                {
                    return RedirectToAction("Marcas", new { msg = "O registo não pode ser eliminado enquanto houver modelos" });


                }
            }
        }

        [AllowAnonymous]
        public ActionResult EditMarca(int? id)
        {
            using (standEntities bd = new standEntities())
            {
                marca m = bd.marcas.Find(id);
                if (m != null)
                {
                    return View(m);
                }
                return RedirectToAction("Marcas", new { msg = "Marca não existe" });

            }

        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult EditMarca(marca m, HttpPostedFileBase fich)
        {
            using (standEntities bd = new standEntities())
            {
                marca editado = bd.marcas.Find(m.codmarca);
                if (editado != null)
                {
                    editado.marca1 = m.marca1;
                    if (fich != null && fich.ContentLength > 0 && fich.ContentType.Contains("image"))
                    {
                        System.Drawing.Image img = System.Drawing.Image.FromStream(fich.InputStream);
                        float altura = (40 * img.PhysicalDimension.Height) / img.PhysicalDimension.Width;
                        img = img.GetThumbnailImage(40, (int)altura, null, IntPtr.Zero);
                        MemoryStream memo = new MemoryStream();
                        img.Save(memo, System.Drawing.Imaging.ImageFormat.Png);
                        editado.logotipo = memo.ToArray();

                    }
                    bd.SaveChanges();
                    return RedirectToAction("Marcas", new { msg = "Registado com sucesso" });
                }

                return RedirectToAction("Marcas", new { msg = "Marca não existe" });

            }

        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult CreateMarca()
        {

            marca m = new marca();
            return View(m);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult CreateMarca(marca m, HttpPostedFileBase fich)
        {
            using (standEntities bd = new standEntities())
            {
                if (fich != null && fich.ContentLength > 0 && fich.ContentType.Contains("image"))
                {
                    System.Drawing.Image img = System.Drawing.Image.FromStream(fich.InputStream);
                    float altura = (40 * img.PhysicalDimension.Height) / img.PhysicalDimension.Width;
                    img = img.GetThumbnailImage(40, (int)altura, null, IntPtr.Zero);
                    MemoryStream memo = new MemoryStream();
                    img.Save(memo, System.Drawing.Imaging.ImageFormat.Png);
                    m.logotipo = memo.ToArray();
                }
                bd.marcas.Add(m);
                bd.SaveChanges();
                return RedirectToAction("Marcas");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Marcas(String msg)
        {
            using (standEntities bd = new standEntities())
            {
                ViewBag.msg = msg;
                List<marca> marcas = bd.marcas.ToList();
                return View(marcas);
            }


        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ListarMarcas()
        {
            using (standEntities bd = new standEntities())
            {
                List<marca> marcas = bd.marcas.ToList();
                return View(marcas);
            }
        }


        public ActionResult AddMarca()
        {
            marca m = new marca();
            return View(m);
        }

        public ActionResult EditarMarca(int? id)
        {
            using (standEntities bd = new standEntities())
            {
                marca m = bd.marcas.Find(id);
                if (m != null) return View(m);
                else return RedirectToAction("ListarMarcas");
            }
        }

        [HttpPost]
        public ActionResult EditarMarca(marca novo, HttpPostedFileBase fich)
        {
            using (standEntities bd = new standEntities())
            {
                marca m = bd.marcas.Find(novo.codmarca);
                if (m != null)
                {
                    m.marca1 = novo.marca1;
                    if (fich != null && fich.ContentLength > 0 && fich.ContentType.Contains("image"))
                    {
                        System.Drawing.Image img = System.Drawing.Image.FromStream(fich.InputStream);
                        float altura = img.PhysicalDimension.Height * 40 / img.PhysicalDimension.Width;
                        img = img.GetThumbnailImage(40, (int)altura, null, new IntPtr());
                        MemoryStream memo = new MemoryStream();
                        img.Save(memo, System.Drawing.Imaging.ImageFormat.Png);
                        m.logotipo = memo.ToArray();

                    }

                    bd.SaveChanges();
                }

            }
            return RedirectToAction("ListarMarcas");
        }

        [HttpPost]
        public ActionResult AddMarca(marca novo, HttpPostedFileBase fich)
        {
            using (standEntities bd = new standEntities())
            {

                if (fich != null && fich.ContentLength > 0 && fich.ContentType.Contains("image"))
                {
                    System.Drawing.Image img = System.Drawing.Image.FromStream(fich.InputStream);
                    MemoryStream memo = new MemoryStream();
                    img.Save(memo, System.Drawing.Imaging.ImageFormat.Png);
                    novo.logotipo = memo.ToArray();

                }
                bd.marcas.Add(novo);
                bd.SaveChanges();


            }
            return RedirectToAction("ListarMarcas");
        }
    }
}